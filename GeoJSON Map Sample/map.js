
var map;
var view;
var token;
var token_expires;
const base_url = "https://rjn-clarity-api.com/v1/clarity";

require(["esri/Map", "esri/views/MapView", "esri/layers/GeoJSONLayer", "esri/widgets/FeatureTable"], (Map, MapView, GeoJSONLayer, FeatureTable) => {


    map = new Map({
        basemap: "topo-vector"
    });

    view = new MapView({
        container: "viewDiv",
        map: map,
        zoom: 4,
        center: [-95.712891, 37.090240] // longitude, latitude
    });

    LoadLayers = () => {
        let additional_manhole_attributes = ["InspectionStatus"];

        const url = `${base_url}/StructureInspection/geojson?attribute_list=${additional_manhole_attributes.join()}&token=${token}`;
        const start_time = new Date();

        LogStep("Loading Layer @ " + url);

        let manhole_layer = new GeoJSONLayer({
            url: url,
            copyright: "RJN Group, Inc",
            displayField: "name",
            title: "Manhole Inspections",
            outFields: ["name", "id", ...additional_manhole_attributes],
            renderer: manhole_renderer,
            popupTemplate: { // autocasts as new PopupTemplate(),
                title: "{name}",
                content: "<p>Status: {InspectionStatus}</p> <p>Wall Material: {WallMaterial}{macp_wallmaterial}</p>",
                overwriteActions: true
            },
            //  labelingInfo: [labelClass]
        });

        map.add(manhole_layer);

        //Wait for the layer to load
        manhole_layer.when(() => {
            LogStep("Loaded Layer - " + (new Date().valueOf() - start_time.valueOf()) + "ms");
            return manhole_layer.queryExtent();
        }).then((response) => {
            //Zoom to the extent of the layer.
            view.goTo(response.extent);
        }).catch((err) => {
            var time_ms = (new Date().valueOf() - start_time.valueOf())
            LogStep("Failed to Load Layer - " + (time_ms > 30000 ? ("Timed Out - ") : err + " - ") + time_ms + "ms");
        });


        // Create the feature table
        document.getElementById("tableDiv").innerHTML = "";
        const featureTable = new FeatureTable({
            view: view,
            layer: manhole_layer,
            multiSortEnabled: true, // set this to true to enable sorting on multiple columns
            editingEnabled: false,
            // Autocast the FieldColumnConfigs
            fieldConfigs: ["name", "id", ...additional_manhole_attributes].map((attribute, index) => {
                return index == 0 ?
                            {
                                name: attribute.toLocaleLowerCase(),
                                label: attribute,
                                direction: "asc"
                            }
                            :
                            {
                                name: attribute.toLocaleLowerCase(),
                                label: attribute
                            }                       
            }) 
            ,
            container: document.getElementById("tableDiv")
        });
    }
});


function LoadLayers() { } //Just a placeholder

async function AuthenticateAndLoad() {
    ClearConsole();
    await Authenticate();
    LoadLayers();
}

async function Authenticate() {

    var client_id = document.getElementById("client-id").value;
    var password = document.getElementById("p-word").value;
    var url = base_url + "/auth";

    LogStep("Authenticating...")

    //Send a post request to authenticate and generate a token
    await fetch(url, {
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

        LogStep("Got token: " + token);
    })
    .catch(err => {
        LogStep("Failed to authenticate: " + err);
    });

}

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

const manhole_renderer = {
    type: "unique-value",  // autocasts as new UniqueValueRenderer()
    field: "InspectionStatus",
    defaultSymbol: {
        type: "simple-fill",

        color: "black"
    },  // autocasts as new SimpleFillSymbol()
    uniqueValueInfos: [{
        // All features with value of "North" will be blue
        value: "Inspected",
        symbol: {
            type: "simple-fill",  // autocasts as new SimpleFillSymbol()
            color: "blue"
        }
    }, {
        // All features with value of "East" will be green
        value: "East",
        symbol: {
            type: "simple-fill",  // autocasts as new SimpleFillSymbol()
            color: "green"
        }
    }, {
        // All features with value of "South" will be red
        value: "South",
        symbol: {
            type: "simple-fill",  // autocasts as new SimpleFillSymbol()
            color: "red"
        }
    }, {
        // All features with value of "West" will be yellow
        value: "West",
        symbol: {
            type: "simple-fill",  // autocasts as new SimpleFillSymbol()
            color: "yellow"
        }
    }]
};
