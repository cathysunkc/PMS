<%@ Page Title="Reporting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reporting.aspx.cs" Inherits="PMS.Reporting" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Login</title>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
    <!--Load Google Chart API-->
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
        <!--Hide Header and Print Button when printing-->
        <style type="text/css" media="print">
        @page {
            size: auto;   /* auto is the initial value */
            margin: 0mm;  /* this affects the margin in the printer settings */
        }
        header, #btnPrint {
            display: none;
        }    
    </style>
    <script type="text/javascript">
        /********************************/
        /* Sales Table                  */
        /********************************/
        /*
        google.charts.load('current', { 'packages': ['table'] });
        google.charts.setOnLoadCallback(drawSalesTable);

        function drawSalesTable() {
            var data = new google.visualization.DataTable();
            data.addColumn('string', '');
            data.addColumn('string', 'Unit');
            data.addColumn('string', 'Ratio');
            data.addRows(<%= this.GetSalesTableValue() %>);

           /* var options = {
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
        }*/
        //google.charts.load('current', { packages: ['corechart', 'bar'] });
        //google.charts.load('current', { packages: ['corechart', 'line'] });

        //Load Google Charts
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.load('current', { 'packages': ['barChart'] }); 
        /********************************/
        /* Sales by Period Chart        */
        /********************************/
       
        google.charts.setOnLoadCallback(drawSalesByPeriodChart);

        function drawSalesByPeriodChart() {
            var data = google.visualization.arrayToDataTable(<%= GetSalesByPeriodValue() %>);            

            var options = {
                title: "Sales by Period",
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
            var salesByPeriodChart = new google.visualization.ColumnChart(document.getElementById("sales_period_chart_div"));
            salesByPeriodChart.draw(data, options);
        }

        /********************************/
        /* Sales by Property Type Chart  */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesByPropertyTypeChart);

        function drawSalesByPropertyTypeChart() {

            var data = google.visualization.arrayToDataTable(<%= GetSalesPropertyTypeValue() %>);

            var options = {
                title: 'Sales by Property Type',
                colors: ['#5E2D79'], 
                height: 300,  
                legend: 'none',
            };

            var salesByPropertyTypeChart = new google.visualization.BarChart(document.getElementById('property_type_chart_div'));

            salesByPropertyTypeChart.draw(data, options);
        }

        /********************************/
        /* Sales Price by Period Chart  */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesPropertyPriceChart);

        function drawSalesPropertyPriceChart() {
            var data = google.visualization.arrayToDataTable(<%= GetSalesPriceByPeriodValue() %>);

            var options = {
                title: 'Sales Average Price',
                colors: ['#5E2D79'],
                curveType: 'function',
                height: 300,  
                legend: 'none',
                vAxis: {
                    format: '$#,###'
                },
            };

            var salesPropertyPriceChart = new google.visualization.LineChart(document.getElementById('property_price_chart_div'));

            salesPropertyPriceChart.draw(data, options);
        }       
        
        /********************************/
        /* Sales Percent Chart          */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesPercentChart);

        function drawSalesPercentChart() {

            var data = google.visualization.arrayToDataTable(<%= this.GetSalesPercentValue() %>);

            var options = {
                title: 'Sales Ratio',
                width: '100%',
                height: 300,
                colors: ['#5E2D79', '#903841'],
                backgroundColor: {
                    fill: '#FFFFFF'
                },

            };

            var salesPercentChart = new google.visualization.PieChart(document.getElementById('sales_percent_chart_div'));
            salesPercentChart.draw(data, options);
        } 
    </script>
        <asp:Panel ID="panelReport" runat="server">
                <div style="width: 50%; float: left">
                    <h2>Sales Report</h2></div>
                    <div style="width: 50%; float: right"><button onclick="window.print()" id="btnPrint" class="form-button" style="width:120px; float: right">Print Report</button>
                </div>
                <div style="width: 100%; float: left; margin-bottom: 10px">
                    <asp:Label ID="lblFrom" runat="server" Text="From " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblStartDate" runat="server" Text="StartDate"></asp:Label>
                    <asp:Label ID="lblTo" runat="server" Text=" to " Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblEndDate" runat="server" Text="EndDate"></asp:Label> 
                   </div>   
                <div id="sales_table_div" style="color:black"></div>
                <div style="width: 33%;float: left; text-align: center;">
                  <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                    <asp:Label ID="Label1" runat="server" ForeColor="black" Font-Bold="true" Text="Total Listings"></asp:Label>
                    <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblTotalListing" runat="server" Text="Total Listing"></asp:Label></div>
                  </div>
                </div>
               <div style="width: 33%;float: left;text-align: center;">
                  <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                      <asp:Label ID="Label2" runat="server" ForeColor="black" Font-Bold="true" Text="Total Sales"></asp:Label>
                      <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblTotalSales" runat="server" Text="Total Sales"></asp:Label></div>
                  </div>
                </div>
                <div style="width: 33%;float: left;text-align: center;">
                  <div style="background: white;margin-right: 3px;margin-bottom: 20px;padding: 10px;">
                    <asp:Label ID="Label3" runat="server" ForeColor="black" Font-Bold="true" Text="Avg. Sales Price"></asp:Label>
                    <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblAvgPrice" runat="server" Text="Average Price"></asp:Label></div>
                   </div>
                </div>
                <br/><br/>
                <div>
                    <div id="sales_percent_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                    <div id="sales_period_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
               </div>
                    <div id="property_type_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                   <div id="property_price_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
         </asp:Panel> 
        <br/><br/>
    </main>
</asp:Content>
