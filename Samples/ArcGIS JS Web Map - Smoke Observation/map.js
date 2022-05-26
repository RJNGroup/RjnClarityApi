
var map;
var view;
var token;
var token_expires;
const base_url = "https://rjn-clarity-api.com/v1/clarity";



require(["esri/Map", "esri/views/MapView", "esri/layers/GeoJSONLayer", "esri/widgets/FeatureTable", "esri/widgets/Legend"], (Map, MapView, GeoJSONLayer, FeatureTable, Legend) => {


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
            { name: "streetname", label: "Street" }
        ];

        //Create the URL, we can add the additional attributes as a query parameter (and don't forget the token!)
        const url = `${base_url}/SmokeObservation/geojson?attribute_list=${additional_smoke_attributes.map(a => a.name).join(",")}&token=${token}`;

        const start_time = new Date();

        LogStep("Loading Layer @ " + url);

        //Define the layer
        let smoke_layer = new GeoJSONLayer({
            url: url,
            copyright: "RJN Group, Inc",
            displayField: "name",
            title: "Smoke Observations",
            outFields: ["name", "id", ...additional_smoke_attributes.map(a => a.name)],
            renderer: smoke_renderer,
            popupTemplate: { // autocasts as new PopupTemplate(),
                title: "{name}",
                content: "<p>Observation: {Observation}</p> <p>Intensity: {SmokeIntensity}</p>",
                overwriteActions: true
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


//Defines the layer styling styling
const smoke_renderer = {
    type: "unique-value",  
    field: "Observation",
    legendOptions: { title: "Location Code" },
    defaultSymbol: {
        type: "simple-marker",
        color: [215, 215, 215, 0.5],
        outline: {width: 0.5}
    },  
    uniqueValueInfos: [
    {
        
        value: "Main Highway - Urban",
        symbol: {
            type: "simple-marker",  
            color: [90, 0, 0, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Main Highway - Suburban",
        symbol: {
            type: "simple-marker",  
            color: [150, 50, 50, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Light Highway",
        symbol: {
            type: "simple-marker",  
            color: [210, 100, 100, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Railway",
        symbol: {
            type: "simple-marker",  
            color: [107, 0, 103, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Sidewalk",
        symbol: {
            type: "simple-marker",  
            color: [185, 185, 185, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Parking Lot",
        symbol: {
            type: "simple-marker",  
            color: [20, 20, 20, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Easement/Right of Way",
        symbol: {
            type: "simple-marker",  
            color: [40, 110, 40, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Yard",
        symbol: {
            type: "simple-marker",  
            color: [57, 179, 56, 1],
            outline: {width: 0.5}
        }
    }, {
        
        value: "Ditch",
        symbol: {
            type: "simple-marker",  
            color: [57, 95, 170, 1],
            outline: {width: 0.5}
        }
    },
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

