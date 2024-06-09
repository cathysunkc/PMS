<%@ Page Title="Reporting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="PMS.Reporting" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Login</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <!--Load the AJAX API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

        /********************************/
        /* Pie Chart                    */
        /********************************/
        google.charts.load('current', {'packages':['corechart']});
        google.charts.setOnLoadCallback(drawPieChart);

        function drawPieChart() {

        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Status');
        data.addColumn('number', 'Number');
        data.addRows(<%= this.GetPieChartValue() %>);

        var options = {
            title: 'Percentage of Sales',
            width:'100%',
            height: 300,
            borderRadius: 5,
            colors: ['#5E2D79', '#903841'],
            backgroundColor: {
                fill: '#FFFFFF'
            },
        };

        var piechart = new google.visualization.PieChart(document.getElementById('pie_chart_div'));
        piechart.draw(data, options);
        }

        /********************************/
        /* Table Chart                  */
        /********************************/
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawTable);

        function drawTable() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Status');
            data.addColumn('number', 'Number');
            data.addColumn('string', 'Percentage');
            data.addRows(<%= this.GetTableValue() %>);

            var table = new google.visualization.Table(document.getElementById('table_div'));            
            table.draw(data, { showRowNumber: true, width: '100%', height: '100%' } );
        }

        /********************************/
        /* Bar Chart                  */
        /********************************/
        google.charts.load('current', { 'packages': ['barChart'] });
        google.charts.setOnLoadCallback(drawBarChart);

        function drawBarChart() {
            var data = google.visualization.arrayToDataTable([
                ["Element", "Sales"],
                ["2024 Mar", 2],
                ["2024 Apr", 3],
                ["2024 May", 4],
                ["2024 Jun", 1]
            ]);            

            var options = {
                title: "Number of Sales",
                width: '50%',      
                height: 300,
            };
            var barChart = new google.visualization.ColumnChart(document.getElementById("bar_chart_div"));
            barChart.draw(data, options);
        }

    </script>
        <asp:Panel ID="panelReport" runat="server">
           
                <h2>Sales Reports</h2>
                <br/><br/>
                <div id="table_div" style="color:black"></div><br/><br/>
                <div style="display: table; width: 100%;  table-layout:auto;">
                    <div id="pie_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                    <div id="bar_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                </div>
             
        </asp:Panel>  
        <br/><br/>
    </main>
</asp:Content>
