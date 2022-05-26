
var map;
var view;
var token;
var token_expires;
const base_url = "https://rjn-clarity-api.com/v1/clarity";

require(["esri/Map", 
        "esri/views/MapView", 
        "esri/layers/GeoJSONLayer", 
        "esri/widgets/FeatureTable", 
        "esri/widgets/Legend", 
        "esri/popup/content/CustomContent"], 
        (Map, MapView, GeoJSONLayer, FeatureTable, Legend, CustomContent) => {


    map = new Map({
        basemap: "topo-vector"
    });

    view = new MapView({
        container: "viewDiv",
        map: map,
        zoom: 4,
        center: [-95.712891, 37.090240] // longitude, latitude
    });

    //Add a legend
    let legend = new Legend({
        view: view
    });
    view.ui.add(legend, "bottom-right");

    LoadLayers = () => {

        /*Any additional attributes that are available on your project can be loaded with the layer.
        Also, keep in mind, if your inspections are not MACP this demo will not work well and you may want to select another attribute to render on.
        Available attributes can be queried by calling the API route: https://rjn-clarity-api.com/v1/clarity/projects/{{projectid}}/SmokeObservation/attributes

        IMPORTANT: caution should be taken not to overdo this feature and cause the layer to exceed the 30s timeout window.
        */
        let additional_smoke_attributes = [
            { name: "drainagearea", label: "Drainage Area (ft)" },
            { name: "streetnumber", label: "Address" },
            { name: "streetname", label: "Street" }, 
            { name: "medialist", label: "medialist"}
        ];

        //Create the URL, we can add the additional attributes as a query parameter (and don't forget the token!)
        const url = `${base_url}/SmokeObservation/geojson?attribute_list=${additional_smoke_attributes.map(a => a.name).join(",")}&token=${token}`;

        const start_time = new Date();

        LogStep("Loading Layer @ " + url);

        //Define the popup content]
        /*
        var content = (event) => {
                var observation = event.attributes.observation;
                var intensity = event.attributes.smokeintensity;
                var medialist = JSON.parse(event.attributes.medialist);

                console.log(event)
                return Promise.all(medialist.map(m => GetMediaURL(m.id).then((url) => m["url"] = url)))
                    .then((value) => {
                        
                    console.log(medialist)

                        return `
                        <p>Observation: ${observation}</p> 
                        <p>Intensity: ${intensity}</p>
                        ${medialist.filter(m => m.type == "Photo").map(m => "<img src='" + m.url + "'>")}
                `
                    });

                
            };      */
        

        //Define the layer
        let smoke_layer = new GeoJSONLayer({
            url: url,
            copyright: "RJN Group, Inc",
            displayField: "name",
            title: "Smoke Observations",
            outFields: ["name", "id", ...additional_smoke_attributes.map(a => a.name)],
            renderer: smoke_renderer,
            popupTemplate: { // autocasts as new PopupTemplate(),
                outFields: ["*"],
                title: "{name}",
                content: (event) => {
                    var observation = event.graphic.attributes.observation;
                    var intensity = event.graphic.attributes.smokeintensity;
                    var medialist = JSON.parse(event.graphic.attributes.medialist);
    
                    return Promise.all(medialist.map(m => GetMediaURL(m.id).then((url) => m["url"] = url)))
                        .then((value) => {    
                            return `
                            <p>Observation: <strong>${observation}</strong></p> 
                            <p>Intensity: <strong>${intensity}</strong></p>
                            <hr>
                            ${ medialist.filter(m => m.type == "Photo")
                                .map(m => `<figure>
                                                <img src='${m.url}'>
                                                <figcaption>${m.name}</figcaption>
                                            </figure>`).join("")
                            }
                            `;
                        });           
                }
            },
        });

        //Add the layer
        map.add(smoke_layer);

        //Wait for the layer to load
        smoke_layer.when(() => {
            LogStep("Loaded Layer - " + (new Date().valueOf() - start_time.valueOf()) + "ms");
            return smoke_layer.queryExtent();
        }).then((response) => {
            //Zoom to the extent of the layer.
            view.goTo(response.extent);
        }).catch((err) => {
            var time_ms = (new Date().valueOf() - start_time.valueOf())
            LogStep("Failed to Load Layer - " + (time_ms > 30000 ? ("Timed Out - ") : err + " - ") + time_ms + "ms");
        });



        //Just in case we already loaded it, clear the table div first!
        document.getElementById("tableDiv").innerHTML = "";

        //Create the feature table
        /*
            Because I have to refer to the additional attributes several times in this function, I decided to define them once in the 
            additional_smoke_attributes array variable above. That makes the fieldConfigs definition a bit more complex than 
            the example on ESRI's site. It is not necessary to do it this way, I just like to define things once in case I wish
            to change it later.
        */
        const featureTable = new FeatureTable({
            view: view,
            layer: smoke_layer,
            multiSortEnabled: true, // set this to true to enable sorting on multiple columns
            editingEnabled: false,
            columnReorderingEnabled : true,
            highlightOnRowSelectEnabled: true,
            fieldConfigs: ["id", "observation", "smokeintensity", ...additional_smoke_attributes.map(a => a.name)]
                    .filter(a => a != "medialist")
                    .map(attribute => {
                            var added = additional_smoke_attributes.find(a => a.name == attribute); //See if this is one of the additional attributes defined 
                            var label = added ? added.label : attribute; //if it is, get the "nice" label we defined above.
                            return attribute == "name" ?
                                {
                                    name: "name",
                                    label: "Smoke Defect",
                                    direction: "asc"
                                }
                                :
                                {
                                    name: attribute,
                                    label: label
                                }
                            })
            ,
            container: document.getElementById("tableDiv")
        });
    }
});



function LoadLayers() { } //Just a placeholder


//This is the Load button click handler.
async function AuthenticateAndLoad() {
    ClearConsole();
    if (await Authenticate()) LoadLayers();
}

//This generates the token
async function Authenticate() {

    var client_id = document.getElementById("client-id").value;
    var password = document.getElementById("p-word").value;
    var url = base_url + "/auth";
    const start_time = new Date();

    LogStep("Authenticating...")

    //Send a post request to authenticate and generate a token
    return await fetch(url, {
        method: "POST",
        headers: {
            'Accept': 'application/json'
        },
        body: JSON.stringify({ client_id: client_id, password: password }) //<-- This is the payload that must be sent to authenticate
    })
        .then(async (response) => {
            var credentials = await response.json(); //<-- Get the response JSON
            token = credentials.token;
            token_expires = credentials.expires;

            LogStep("Got token - " + (new Date().valueOf() - start_time.valueOf()) + 'ms');
            return true;
        })
        .catch(err => {
            LogStep("Failed to authenticate --- " + err);
            return false;
        });

}

async function GetMediaURL(mediaid) {
    let url = base_url + "/media/" + mediaid + "?token=" + token;
    return await fetch(url)
            .then((res) => {
                return res.text();
            } )
            .catch((err) => {
                console.error(err)
            });
}

//If I color by EVERY observation type, the map is going to get crazy.
//We can combine several of the observations that are similar (such as public and private cleanouts or laterals)
//and wind up with a more coherent map.
const ObservationsSimplified = [
    {name: "Private Drain", color: [217, 95, 20], obs: ["Area Drain", "Driveway Drain", "Patio Drain", "Window Well Drain", "Stairwell Drain"]},
    {name: "Cleanout", color: [73, 104, 104], obs: ["Cleanout Defective", "Cleanout Defective, Public", "Cleanout, Missing/Broken Cap", "Cleanout Missing/Broken Cap, Public"]},
    {name: "Downspout", color: [177, 114, 42], obs: ["Downspout", "Downspout, Disconnected", "Downspout U G (Multi Defects)"]},
    {name: "Foundation Drain", color: [90, 22, 22], obs: ["Foundation Drain"]},
    {name: "Lateral", color: [6, 21, 67], obs: ["Lateral", "Lateral, Public", "Sidewalk", "Curb and Gutter", "Water Valve Box", "Storm Ditch"]},
    {name: "Mainline", color: [9, 55, 87], obs: ["Mainline (Multiple Defects)", "Sanitary Mainline"]},
    {name: "Manhole", color: [80, 132, 155],obs: ["Manhole (Pick Holes)", "Manhole Upstream", "Private Sanitary Manhole", "Manhole Cover", "Manhole Downstream"]},
    {name: "Storm Connection", color: [255, 38, 60], obs: ["Storm Manhole", "Storm Sewer Cleanout", "Catch Basin", "Private Storm Manhole", "Storm Inlet"]},
    {name: "Sump Pump", color: [164, 75, 75], obs: ["Sump Pump"]},
];


//Defines the layer styling styling
const smoke_renderer = {
    type: "unique-value",  
    valueExpression: 
            //Create an arcade expression that looks up the simplified defect name from the actual Observation field
            `return When(${ObservationsSimplified.map(o => o.obs.map(obs => "$feature.observation == '" + obs + "','" + o.name + "'").join(',')).join(",")}, 'Other');`
            ,
    legendOptions: { title: "Location Code" },
    defaultSymbol: {
        type: "simple-marker",
        color: [215, 215, 215],
        outline: {width: 0.5, color: [100, 100, 100]}
    },  

    //Color based on observation type
    uniqueValueInfos: ObservationsSimplified.map(o => {
                                      return {
                                          value: o.name,
                                          symbol: {
                                              type: "simple-marker",
                                              color: o.color,
                                              outline: {width: 0.5, color: [100, 100, 100]}
                                          }
                                      };  
                                    }),
    //Size based on Smoke Intensity
    visualVariables: [
        {
            type: "size",
            valueExpression: "return When($feature.smokeintensity == 'Low', 1, $feature.smokeintensity == 'Medium', 2, $feature.smokeintensity == 'High', 3, 0);",
            stops: [
                {value: 1, size: 5, label: "Low"},
                {value: 2, size: 10, label: "Medium"},
                {value: 3, size: 20, label: "High"},
            ]
        }
    ]
    
 
};

function ClearConsole() {
    var c = document.getElementById("console");
    c.innerHTML = "";
    if (!c.classList.contains("show-it")) c.classList.add("show-it");
}

function LogStep(msg) {
    var node = document.createElement("p");
    node.innerText = msg
    document.getElementById("console").appendChild(node)
    console.log(msg);
}

