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
        /* Sales Table                  */
        /********************************/
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawSalesTable);

        function drawSalesTable() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', '');
            data.addColumn('string', 'Unit');
            data.addColumn('string', 'Ratio');
            data.addRows(<%= this.GetSalesTableValue() %>);

            var options = {
                showRowNumber: true, width: '100%', height: '100%',
                allowHtml: true,
                format: {
                    columns: {
                        0: { align: 'left' },
                        1: { align: 'left' },
                        2: { align: 'right' },
                        3: { align: 'right' },
                    }
                }
            }

            var salesTable = new google.visualization.Table(document.getElementById('sales_table_div'));
            salesTable.draw(data, options);
        }

        /********************************/
        /* Sales Pie Chart              */
        /********************************/
        google.charts.load('current', {'packages':['corechart']});
        google.charts.setOnLoadCallback(drawSalesPieChart);

        function drawSalesPieChart() {

        var data = google.visualization.arrayToDataTable(<%= this.GetSalesPieChartValue() %>);

        var options = {
            title: 'Sales Ratio',   
            width: '100%',
            height: '100%',  
            colors: ['#5E2D79', '#903841'],
            backgroundColor: {
                fill: '#FFFFFF'
            },
        };

            var salesPieChart = new google.visualization.PieChart(document.getElementById('sales_pie_chart_div'));
            salesPieChart.draw(data, options);
        }

        

        /********************************/
        /* Sales Period Bar Chart    */
        /********************************/
        google.charts.load('current', { 'packages': ['barChart'] });
        google.charts.setOnLoadCallback(drawSalesPeriodBarChart);

        function drawSalesPeriodBarChart() {
            var data = google.visualization.arrayToDataTable(<%= GetSalesPeriodChartValue() %>);            

            var options = {
                title: "Unit Sales by Period",
                width: '100%',      
                height: 300,  
                colors: ['#5E2D79'], 
                legend: 'none',
                series: {
                    0: { 
                        dataLabel: {
                            textStyle: {
                                fontSize: 8 
                            }
                        }
                    }
                },
                
            };
            var chart = new google.visualization.ColumnChart(document.getElementById("sales_period_chart_div"));
            chart.draw(data, options);
        }

        /********************************/
        /* Property Type Chart          */
        /********************************/
        google.charts.load('current', { packages: ['corechart', 'bar'] });
        google.charts.setOnLoadCallback(drawBasic);

        function drawBasic() {

            var data = google.visualization.arrayToDataTable(<%= GetPropertyTypeChartValue() %>);

            var options = {
                title: 'Unit Sales by Property Type',
                colors: ['#5E2D79'], 
                chartArea: { width: '50%' },
                legend: 'none',
            };

            var chart = new google.visualization.BarChart(document.getElementById('property_type_chart_div'));

            chart.draw(data, options);
        }

    </script>
        <asp:Panel ID="panelReport" runat="server">
           
                <h2>Sales Report</h2>
                <br/>
                <asp:Label ID="lblFrom" runat="server" Text="Listing From: " Font-Bold="true"></asp:Label><br/>
                <asp:Label ID="lblFromDate" runat="server" Text="FromDate"></asp:Label>
                <asp:Label ID="lblTo" runat="server" Text=" to " Font-Bold="true"></asp:Label>
                <asp:Label ID="lblToDate" runat="server" Text="ToDate"></asp:Label>
                <br/><br/>
                <div id="sales_table_div" style="color:black"></div>
                <br/><br/>
                <div style="display: table; width: 100%;  table-layout:auto;">
                    <div id="sales_pie_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                    <div id="sales_period_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                </div><br/>
                <div style="display: table; width: 100%;  table-layout:auto;">
                    <div id="property_type_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                   <div id="column2_chart_div" style="display: table-cell; width: 45%; height: 300px;"></div>
                </div>
             
        </asp:Panel>  
        <br/><br/>
    </main>
</asp:Content>
