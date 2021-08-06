//Status of Watercourses
$(document).ready(function () {
    $.ajax({
        url: '/WaterCourses/LoadAllDistricts',
        type: "GET",
        dataType: "JSON",
        success: function (data) {
            AllFilterData = data;

            $(data.Districts).each(function () {
                //this refers to the current item being iterated over
                var option = $('<option />');
                option.attr('value', this.Id).text(this.Name);
                $('#ddlDistrict').append(option);

                var option1 = $('<option />');
                option1.attr('value', this.Id).text(this.Name);
                $('#dllAddDistrict').append(option1);
            });


        }
    });


    $("#ddlDistrict").on("change", onddlDistrictChange);
    function onddlDistrictChange() {
        //debugger;
        var DistrictId = $('#ddlDistrict').val();
        loadDistrictWiseCharts(DistrictId);
    }



    $.ajax({
        url: '/HEIS/GetStatusOfWatercourses',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //debugger;
            //console.log(DataObject);
            var config = {
                type: 'pie',
                data: {
                    datasets: [{
                        data: [
                            DataObject.TotalWaterCourse,
                            DataObject.ImprovedWaterCourse,
                            DataObject.UnImprovedWaterCourse
                        ],
                        backgroundColor: [
                            window.chartColors.red,
                            window.chartColors.orange,
                            window.chartColors.yellow,
                            window.chartColors.green,
                        ],
                        label: 'Status of Watercourses'
                    }],
                    labels: [
                        "Total",
                        "Improved",
                        "Un-Improved"
                    ]
                },
                options: {
                    responsive: true
                }
            };
            var ctx = document.getElementById("chartjs_pie").getContext("2d");
            window.myPie = new Chart(ctx, config);
        }
    });
});

//$(document).ready(function() 
//{
//	var color = Chart.helpers.color;
//    var barChartData = {
//        labels: ["January", "February", "March", "April", "May", "June", "July"],
//        datasets: [{
//            type: 'bar',
//            label: 'Watercourse Improvement Status',
//            backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
//            borderColor: window.chartColors.red,
//            data: [
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor()
//            ]
//        }, {
//            type: 'line',
//            label: 'Dataset 2',
//            backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
//            borderColor: window.chartColors.blue,
//            data: [
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor()
//            ]
//        }, {
//            type: 'bar',
//            label: 'Dataset 3',
//            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
//            borderColor: window.chartColors.green,
//            data: [
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor(),
//                randomScalingFactor()
//            ]
//        }]
//    };

//        var ctx = document.getElementById("canvas1").getContext("2d");
//        window.myBar = new Chart(ctx, {
//            type: 'bar',
//            data: barChartData,
//            options: {
//                responsive: true,
//                title: {
//                    display: true,
//                    text: 'Overview of Watercourse'
//                },
//            }
//        });
//});

//Watercourse Improvement Status
//chartjs_WatercourseImprovementStatus
$(document).ready(function () {
    var color = Chart.helpers.color;
    $.ajax({
        url: '/HEIS/GetWcImprovementStatus',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //debugger;
            //console.log(DataObject);
            var barChartData = {
                labels: ["Total", "Regu", "Add", "Un-imp"],
                datasets: [{
                    type: 'bar',
                    label: 'Watercourse Improvement Status',
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.orange,
                        window.chartColors.yellow,
                        window.chartColors.green,

                    ],
                    borderColor: window.chartColors.red,
                    data: [
                        DataObject.TotalWaterCourse,
                        DataObject.Regular,
                        DataObject.Addational,
                        DataObject.UnImprovedWaterCourse
                    ]
                }]
            };

            var ctx = document.getElementById("chartjs_WatercourseImprovementStatus").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: ''
                    },
                }
            });

        }
    });


});

//chartjs_LengthOfImprovedWatercourses
$(document).ready(function () {
    var color = Chart.helpers.color;
    $.ajax({
        url: '/HEIS/GetLengthOfImprovedWc',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {

            //console.log(DataObject);
            var barChartData = {
                labels: ["Total Length", "Earthen", "Lined"],
                datasets: [{
                    type: 'bar',
                    label: 'Length of Improved Watercourses (000 KMs)',
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.yellow,
                        window.chartColors.green,

                    ],
                    borderColor: window.chartColors.green,
                    data: [
                        DataObject.TotalLength, DataObject.Earthen, DataObject.Lined
                    ]
                }]
            };

            var ctx = document.getElementById("chartjs_LengthOfImprovedWatercourses").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: ''
                    },
                }
            });
        }
    });
});

//Financial Implication (Rs. Billion)
$(document).ready(function () {
    var color = Chart.helpers.color;
    $.ajax({
        url: '/HEIS/GetImplicationFinancial',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //console.log(DataObject);
            var barChartData = {
                labels: ["Total Cost", "Govt. Assistance", "Farmer Contribution"],
                datasets: [{
                    type: 'bar',
                    label: 'Financial Implication (Rs. Billion)',
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.orange,
                        window.chartColors.green,
                    ],
                    borderColor: window.chartColors.green,
                    data: [
                        DataObject.TotalCost, DataObject.GovtAssistance, DataObject.FarmerContribution
                    ]
                }]
            };

            var ctx = document.getElementById("chartjs_FinancialImplication").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: ''
                    },
                }
            });
        }
    });

});

//canvas_DistrictWiseWaterCourceImprovementStatus
$(document).ready(function () {
    var color = Chart.helpers.color;
    $.ajax({
        url: '/HEIS/GetDistrictWiseWcImprStatus',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //debugger;
            var labels = [];
            var dataAddl = [];
            var dataRegular = [];
            for (let i = 0; i < DataObject.length; ++i) {
                labels.push(DataObject[i].District);
               
                if (typeof DataObject[i].DistrictData.find(x => x.ImprovementType == "Addl.") != 'undefined') {
                    dataAddl.push(DataObject[i].DistrictData.find(x => x.ImprovementType == "Addl.").Value);
                } else {
                    dataAddl.push(0);
                }

                if (typeof DataObject[i].DistrictData.find(x => x.ImprovementType == "Regular") != 'undefined') {
                    dataRegular.push(DataObject[i].DistrictData.find(x => x.ImprovementType == "Regular").Value);
                } else {
                    dataRegular.push(0);
                }

                
            }
            //console.log(DataObject);
            var barChartData = {
                labels: labels,
                datasets: [{
                    type: 'bar',
                    label: 'Regular',
                    backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.red,
                    data: dataRegular
                }, {
                    type: 'bar',
                    label: 'Addtional',
                    backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.blue,
                    data: dataAddl
                }]
            };

            var ctx = document.getElementById("canvas_DistrictWiseWaterCourceImprovementStatus").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'DistrictWise WaterCource Improvement Status'
                    },
                }
            });

        }
    });

});

//Year Wise WaterCource Improvement Trend
$(document).ready(function () {
    var color = Chart.helpers.color;
    $.ajax({
        url: '/HEIS/GetYearWiseWcImprStatus',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //debugger;
            var labels = [];
            var data = [];
            for (let i = 0; i < DataObject.length; ++i) {
                labels.push(DataObject[i].name)
                data.push(DataObject[i].y)
            }
            //console.log(DataObject);
            var barChartData = {
                labels: labels,
                datasets: [{
                    type: 'bar',
                    label: 'Watercourses',
                    backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.green,
                    data: data
                }]
            };

            var ctx = document.getElementById("canvas_YearWiseWaterCourceImprovementStatus").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'DistrictWise WaterCource Improvement Status'
                    },
                }
            });

        }
    });


});

function loadDistrictWiseCharts(districtId) {

    $.ajax({
        url: '/HEIS/GetDistrictWiseOverview',
        type: "GET",
        dataType: "JSON",
        data: {
            districtId: districtId
        },
        success: function (DataObject) {
            //debugger;
            //console.log(DataObject);
            var Regular = 0;
            if (typeof DataObject.KeyValueDtos.find(e => e.name === "Regular") !== 'undefined') {
                Regular = DataObject.KeyValueDtos.find(e => e.name === "Regular").y;
            }

            var Addl = 0;
            if (typeof DataObject.KeyValueDtos.find(e => e.name === "Addl.") !== 'undefined') {
                Regular = DataObject.KeyValueDtos.find(e => e.name === "Addl.").y;
            }

            var config = {
                type: 'pie',
                data: {
                    datasets: [{
                        data: [
                            DataObject.UnImprovedWaterCourse,
                            DataObject.ImprovedWaterCourse
                        ],
                        backgroundColor: [
                            window.chartColors.red,
                            window.chartColors.green,

                        ],
                        label: 'Status of Watercourses'
                    }],
                    labels: [
                        "Un-Improved",
                        "Improved"
                    ]
                },
                options: {
                    responsive: true
                }
            };
            var ctx = document.getElementById("districtWise_UnImprovementStatus").getContext("2d");
            window.myPie = new Chart(ctx, config);
            //===============districtWise_WatercourseImprovementStatus ================//

            var color = Chart.helpers.color;
            var barChartData = {
                labels: ["Total", "Regu", "Add", "Un-imp"],
                datasets: [{
                    type: 'bar',
                    label: 'District wise Watercourse Improvement Status',
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.orange,
                        window.chartColors.yellow,
                        window.chartColors.green,
                    ],
                    borderColor: window.chartColors.red,
                    data: [
                        DataObject.UnImprovedWaterCourse + DataObject.ImprovedWaterCourse,
                        Regular,
                        Addl,
                        DataObject.UnImprovedWaterCourse
                    ]
                }]
            };
            var ctx = document.getElementById("districtWise_WatercourseImprovementStatus").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: ''
                    },
                }
            });

            //===============//districtWise_LengthOfImprovedWatercourses=================//

            var color = Chart.helpers.color;
            var barChartData = {
                labels: ["Total Length", "Earthen", "Lined"],
                datasets: [{
                    type: 'bar',
                    label: 'District wise: Length of Improved Watercourses ( KMs)',
                    backgroundColor: [
                        window.chartColors.red,
                        window.chartColors.yellow,
                        window.chartColors.green,
                    ],
                    borderColor: window.chartColors.green,
                    data: [
                        DataObject.TotalLength, DataObject.Earthen, DataObject.Lined = 22
                    ]
                }]
            };

            var ctx = document.getElementById("districtWise_LengthOfImprovedWatercourses").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: ''
                    },
                }
            });

            //===============districtWise_TehsilWiseWatercourseImprovement=================//

            var color = Chart.helpers.color;
            var labelsTehsil = [];
            var dataImprovedTehsil = [];
            var dataUnImprovedTehsil = [];
            for (let i = 0; i < DataObject.TehsilWiseDtos.length; ++i) {
                labelsTehsil.push(DataObject.TehsilWiseDtos[i].Tehsil);
                dataImprovedTehsil.push(DataObject.TehsilWiseDtos[i].ImprovedWaterCourse);
                dataUnImprovedTehsil.push(DataObject.TehsilWiseDtos[i].UnImprovedWaterCourse);
            }
            console.log(labelsTehsil);
            console.log(dataImprovedTehsil);
            console.log(dataUnImprovedTehsil);
            var barChartData = {
                labels: labelsTehsil,
                datasets: [{
                    type: 'bar',
                    label: 'Improved',
                    backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.red,
                    data: dataImprovedTehsil
                }, {
                    type: 'bar',
                        label: 'UnImproved',
                    backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.blue,
                    data: dataUnImprovedTehsil
                }]
            };

            var ctx = document.getElementById("districtWise_TehsilWiseWatercourseImprovement").getContext("2d");
            window.myBar = new Chart(ctx, {
                type: 'bar',
                data: barChartData,
                options: {
                    responsive: true,
                    title: {
                        display: true,
                        text: 'Tehsil wise WaterCource Improvement Status'
                    },
                }
            });
        }
    });
}

