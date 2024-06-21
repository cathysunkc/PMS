<%@ Page Title="Reporting" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="Reporting.aspx.cs" Inherits="PMS.Reporting" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <title>Reporting</title>
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
        header, #tableSelect01, #mainReportHeader {
            display: none;
        }       
    </style>
    <style type="text/css">
        input[type="radio"] {
                visibility: hidden;
        }

        label {
            cursor: pointer;
            padding-top:0.7em;
            padding-bottom:0.7em;
            margin: 0px;
            width: 10em;
            border-radius: 1em;
            text-align: center;
            font-weight: bold;

        }

        input[type="radio"]:not(:checked) + label {
            
            color: #5E2D79;
            background-color: lightgray;
        }

        input[type="radio"]:checked + label {
            color: #FFFFFF; 
            background-color: #5E2D79;
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
        /* Sales Period Chart           */
        /********************************/
       
        google.charts.setOnLoadCallback(drawSalesPeriodChart);

        function drawSalesPeriodChart() {
            var data = google.visualization.arrayToDataTable(<%= this.GetSalesPeriodChart() %>);            

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
            var salesPeriodChart = new google.visualization.ColumnChart(document.getElementById("sales_period_chart_div"));
            salesPeriodChart.draw(data, options);
        }

        /********************************/
        /* Rent Period Chart            */
        /********************************/

        google.charts.setOnLoadCallback(drawRentPeriodChart);

        function drawRentPeriodChart() {
            var data = google.visualization.arrayToDataTable(<%= this.GetRentPeriodChart() %>);

            var options = {
                title: "Rent by Period",
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
            var rentPeriodChart = new google.visualization.ColumnChart(document.getElementById("rent_period_chart_div"));
            rentPeriodChart.draw(data, options);
        }

        /********************************/
        /* Sales by Property Type Chart */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesPropertyTypeChart);

        function drawSalesPropertyTypeChart() {

            var data = google.visualization.arrayToDataTable(<%= this.GetSalesPropertyTypeChart() %>);

            var options = {
                title: 'Sales by Property Type',
                colors: ['#5E2D79'], 
                height: 300,  
                legend: 'none',
            };

            var salesPropertyTypeChart = new google.visualization.BarChart(document.getElementById('sales_property_type_chart_div'));

            salesPropertyTypeChart.draw(data, options);
        }

        /********************************/
        /* Rent by Property Type Chart  */
        /********************************/

        google.charts.setOnLoadCallback(drawRentPropertyTypeChart);

        function drawRentPropertyTypeChart() {

            var data = google.visualization.arrayToDataTable(<%= this.GetRentPropertyTypeChart() %>);

            var options = {
                title: 'Rent by Property Type',
                colors: ['#5E2D79'],
                height: 300,
                legend: 'none',
            };

            var rentPropertyTypeChart = new google.visualization.BarChart(document.getElementById('rent_property_type_chart_div'));

            rentPropertyTypeChart.draw(data, options);
        }

        /********************************/
        /* Sales Price Chart            */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesPriceChart);

        function drawSalesPriceChart() {
            var data = google.visualization.arrayToDataTable(<%= this.GetSalesPriceChart() %>);

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

            var salesPriceChart = new google.visualization.LineChart(document.getElementById('sales_price_chart_div'));

            salesPriceChart.draw(data, options);
        }    

        /********************************/
        /* Rent Price Chart            */
        /********************************/

        google.charts.setOnLoadCallback(drawRentPriceChart);

        function drawRentPriceChart() {
            var data = google.visualization.arrayToDataTable(<%= this.GetRentPriceChart() %>);

            var options = {
                title: 'Rent Average Price',
                colors: ['#5E2D79'],
                curveType: 'function',
                height: 300,
                legend: 'none',
                vAxis: {
                    format: '$#,###'
                },
            };

            var rentPriceChart = new google.visualization.LineChart(document.getElementById('rent_price_chart_div'));

            rentPriceChart.draw(data, options);
        }   
        
        /********************************/
        /* Sales Percent Chart          */
        /********************************/
        
        google.charts.setOnLoadCallback(drawSalesPieChart);

        function drawSalesPieChart() {

            var data = google.visualization.arrayToDataTable(<%= this.GetSalesPieChart() %>);

            var options = {
                title: 'Sales Ratio',
                width: '100%',
                height: 300,
                colors: ['#5E2D79', '#903841'],
                backgroundColor: {
                    fill: '#FFFFFF'
                },

            };

            var salesPieChart = new google.visualization.PieChart(document.getElementById('sales_pie_chart_div'));
            salesPieChart.draw(data, options);
        } 

        /******************************** /
        /* Rent Percent Chart          */
        /********************************/

        google.charts.setOnLoadCallback(drawRentPieChart);

        function drawRentPieChart() {

            var data = google.visualization.arrayToDataTable(<%= this.GetRentPieChart() %>);

            var options = {
                title: 'Rent Ratio',
                width: '100%',
                height: 300,
                colors: ['#5E2D79', '#903841'],
                backgroundColor: {
                    fill: '#FFFFFF'
                },
            };
            var rentPieChart = new google.visualization.PieChart(document.getElementById('rent_pie_chart_div'));
            rentPieChart.draw(data, options);
        } 
    </script>       
       
            <div style=" float: left;">
                <h2 id="mainReportHeader">Reporting</h2> 
            </div>
             <asp:Panel ID="panelSelect" runat="server">
                 <table id="tableSelect01" style="width: 100%;">
                     <tr>
                         <td style="width: 70%;">
                                <asp:RadioButton ID="rbForSales" OnCheckedChanged="rbReportType_CheckedChanged"  AutoPostBack="true"   Checked="true" CssClass="radion-button"  GroupName="reportType" runat="server" Text="Sales Report" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rbForRent" OnCheckedChanged="rbReportType_CheckedChanged" AutoPostBack="true"    CssClass="radion-button" GroupName="reportType" runat="server" Text="Rent Report" />  
                             </td>
                         <td style="width: 20%"><button onclick="window.print()" id="btnPrint" class="form-button" style="width:120px; float: right; vertical-align:top;">Print Report</button></td>
                     </tr>                                       
                 </table>                 
               <hr/>
            </asp:Panel>
            <asp:Panel ID="panelSalesReport" runat="server">
                <div style=" float: left; margin-bottom: 1em; width: 100%">
                    <h3><asp:Label ID="lblSalesReport" runat="server" Text="Sales Report"></asp:Label></h3>  
                </div>
                <table style="width: 100%; margin-bottom: 1em">
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Posted Date:" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="lblSalesStartDate" runat="server" Text="StartDate"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text=" to " Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSalesEndDate" runat="server" Text="EndDate"></asp:Label>      
                        </td>     
                    </tr>                      
                </table>        
                <div id="sales_table_div" style="color:black"></div>
                <div style="width: 33%;float: left; text-align: center;">
                  <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                    <asp:Label ID="Label1" runat="server" ForeColor="black" Font-Bold="true" Text="Listings For Sale"></asp:Label>
                    <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblSalesTotalListing" runat="server" Text="Total Listing"></asp:Label></div>
                  </div>
                </div>
                <div style="width: 33%;float: left;text-align: center;">
                  <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                      <asp:Label ID="Label2" runat="server" ForeColor="black" Font-Bold="true" Text="Total Sales"></asp:Label>
                      <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblSalesTotalSales" runat="server" Text="Total Sales"></asp:Label></div>
                  </div>
                </div>
                <div style="width: 33%;float: left;text-align: center;">
                  <div style="background: white;margin-right: 3px;margin-bottom: 20px;padding: 10px;">
                    <asp:Label ID="Label3" runat="server" ForeColor="black" Font-Bold="true" Text="Avg. Sales Price"></asp:Label>
                    <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblSalesAvgPrice" runat="server" Text="Average Price"></asp:Label></div>
                   </div>
                </div>
                <br/><br/>
                <div>
                    <div id="sales_pie_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                    <div id="sales_period_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                </div>
                <div id="sales_property_type_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                <div id="sales_price_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
            </asp:Panel> 
            <asp:Panel ID="panelRentReport" runat="server" Visible="false">
                <div style=" float: left; margin-bottom: 1em; width: 100%">
                    <h3><asp:Label ID="Label12" runat="server" Text="Rent Report"></asp:Label></h3>  
                </div>
                 <table style="width: 100%; margin-bottom: 1em">
                    <tr>
                       <td><asp:Label ID="Label13" runat="server" Text="Posted Date:" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblRentStartDate" runat="server" Text="StartDate"></asp:Label>
                        <asp:Label ID="Label15" runat="server" Text=" to " Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblRentEndDate" runat="server" Text="EndDate"></asp:Label>      
                        </td>     
                    </tr>                      
                </table>   
               <div id="rent_table_div" style="color:black"></div>
               <div style="width: 33%;float: left; text-align: center;">
                 <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                   <asp:Label ID="Label4" runat="server" ForeColor="black" Font-Bold="true" Text="Listings For Rent"></asp:Label>
                   <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblRentTotalListing" runat="server" Text="Total Listing"></asp:Label></div>
                 </div>
               </div>
              <div style="width: 33%;float: left;text-align: center;">
                 <div style="background: white;margin-right: 10px;margin-bottom: 20px;padding: 10px;">                      
                     <asp:Label ID="Label8" runat="server" ForeColor="black" Font-Bold="true" Text="Total Rent"></asp:Label>
                     <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblRentTotalSales" runat="server" Text="Total Rent"></asp:Label></div>
                 </div>
               </div>
               <div style="width: 33%;float: left;text-align: center;">
                 <div style="background: white;margin-right: 3px;margin-bottom: 20px;padding: 10px;">
                   <asp:Label ID="Label10" runat="server" ForeColor="black" Font-Bold="true" Text="Avg. Rent Price"></asp:Label>
                   <div style="color:#5E2D79; font-weight: bold; font-size: 2em;padding: 20px"><asp:Label ID="lblRentAvgPrice" runat="server" Text="Average Price"></asp:Label></div>
                  </div>
               </div>
               <br/><br/>
               <div>
                   <div id="rent_pie_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                   <div id="rent_period_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
              </div>
                   <div id="rent_property_type_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
                  <div id="rent_price_chart_div" style="float:left;width: 49%; height: 300px; margin-right: 10px;margin-bottom: 20px;"></div>
           </asp:Panel> 
        <br/><br/>
    </main>
</asp:Content>
