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

extractResults("__reactFiber$w3gxdnoo6ka");





//// Parse HTML table element to JSON array of objects
//function parseHTMLTableElem(tableEl) {
//    const columns = Array.from(tableEl.querySelectorAll('th')).map(it => it.textContent)
//    const rows = tableEl.querySelectorAll('tbody > tr')
//    return Array.from(rows).map(row => {
//        const cells = Array.from(row.querySelectorAll('td'))
//        return columns.reduce((obj, col, idx) => {
//            obj[col] = cells[idx].textContent
//            return obj
//        }, {})
//    })
//}

//function extractResults() {
//    var tables = $('table');

//    var data = {
//        TeamOverall: {},
//        TeamA: {},
//        TeamB: {},
//        TeamC: {},
//        TeamD: {},
//        IndividualA: {},
//        IndividualB: {},
//        IndividualC: {},
//        IndividualD: {}
//    };

//    tables.each(function() {
//        var table = $(this);
//        var team = true;

//        if (table.parent().parent().hasClass('individual')) {
//            team = false;
//        }

//        if (team) {
//            table.children('thead').remove();
//            table.append('<thead><th>Team</th><th>Points</th></thead>')
//        }
//        else {
//            table.append('<thead><th>Name</th><th>Points</th></thead>')
//        }

//        var cat = table.parent().attr('class');
//        console.log(cat);
//        console.log(table);

//        if (team) {
//            switch (cat) {
//                case "category overall":
//                    data.TeamOverall = parseHTMLTableElem(table);
//                case "category a":
//                    data.TeamA = parseHTMLTableElem(table);
//                case "category b":
//                    data.TeamB = parseHTMLTableElem(table);
//                case "category c":
//                    data.TeamC = parseHTMLTableElem(table);
//                case "category d":
//                    data.TeamD = parseHTMLTableElem(table);
//            }
//        }
//        else {
//            switch (cat) {
//                case "category a":
//                    data.IndividualA = parseHTMLTableElem(table);
//                case "category b":
//                    data.IndividualB = parseHTMLTableElem(table);
//                case "category c":
//                    data.IndividualC = parseHTMLTableElem(table);
//                case "category d":
//                    data.IndividualD = parseHTMLTableElem(table);
//            } 
//        }
//    });

//    console.log(JSON.stringify(data));
//}
