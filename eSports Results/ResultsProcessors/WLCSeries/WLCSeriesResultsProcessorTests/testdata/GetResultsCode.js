// load jq
var jq = document.createElement('script');
jq.src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js";
document.getElementsByTagName('head')[0].appendChild(jq);


function extractResults(reactProp) {

    //$(".overall table tr")[0].__reactFiber$sdw7yk1adom.key

    //$($(".overall table tr")[0]).prop('__reactFiber$9jbn1tmxu2').key

    var data = {
        TeamOverall: {},
        TeamA: {},
        TeamB: {},
        TeamC: {},
        TeamD: {},
        IndividualA: {},
        IndividualB: {},
        IndividualC: {},
        IndividualD: {}
    };

    $('table').each(function () {
        var table = $(this);
        var team = true;

        if (table.parent().parent().hasClass('individual')) {
            team = false;
        }

        var cat = table.parent().attr('class');

        if (team) {
            switch (cat) {
                case "category overall":
                    data.TeamOverall = extractDataFromTeamTable(table, reactProp);
                case "category a":
                    data.TeamA = extractDataFromTeamTable(table, reactProp);
                case "category b":
                    data.TeamB = extractDataFromTeamTable(table, reactProp);
                case "category c":
                    data.TeamC = extractDataFromTeamTable(table, reactProp);
                case "category d":
                    data.TeamD = extractDataFromTeamTable(table, reactProp);
            }
        }
        else {
            switch (cat) {
                case "category a":
                    data.IndividualA = extractDataFromIndividualTable(table, reactProp);
                case "category b":
                    data.IndividualB = extractDataFromIndividualTable(table, reactProp);
                case "category c":
                    data.IndividualC = extractDataFromIndividualTable(table, reactProp);
                case "category d":
                    data.IndividualD = extractDataFromIndividualTable(table, reactProp);
            }
        }
    });

    console.log(data);
    console.log(JSON.stringify(data));
}

function extractDataFromTeamTable(table, reactProp) {
    var tableData = [];

    $(table).find('tr').each(function () {
        var row = $(this);

        var rowData = {
            Id: row.prop(reactProp).key,
            Name: row.find('.teampoints span')[0].textContent,
            Points: row.find('.points')[0].textContent
        };

        tableData.push(rowData);
    });

    return tableData;
}


//extractDataFromTeamTable($(".overall table"), "__reactFiber$9jbn1tmxu2")

function extractDataFromIndividualTable(table, reactProp) {
    var tableData = [];

    $(table).find('tr').each(function () {
        var row = $(this);
        //console.log(row.find('.name').prop(reactProp).return.key);
        var rowData = {
            //Name: row.find('.name')[0].textContent,
            Name: row.find('.name').prop(reactProp).return.key,
            Points: row.find('.points')[0].textContent
        };

        tableData.push(rowData);
    });

    return tableData;
}


//extractDataFromIndividualTable($(".individual .a table"), "__reactFiber$w3gxdnoo6ka")

extractResults("__reactFiber$k3d4wipw1y");