//Status of Watercourses
$(document).ready(function () {
    $.ajax({
        url: '/HEIS/GetStatusOfWatercourses',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            //debugger;
            console.log(DataObject);
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

$(document).ready(function() 
{
	var color = Chart.helpers.color;
    var barChartData = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [{
            type: 'bar',
            label: 'Watercourse Improvement Status',
            backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
            borderColor: window.chartColors.red,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }, {
            type: 'line',
            label: 'Dataset 2',
            backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
            borderColor: window.chartColors.blue,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }, {
            type: 'bar',
            label: 'Dataset 3',
            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
            borderColor: window.chartColors.green,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }]
    };

        var ctx = document.getElementById("canvas1").getContext("2d");
        window.myBar = new Chart(ctx, {
            type: 'bar',
            data: barChartData,
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Overview of Watercourse'
                },
            }
        });
});

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
            console.log(DataObject);
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
                        18302,
                        DataObject[2].y,
                        DataObject[1].y,
                        18080
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
            
            console.log(DataObject);
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
            debugger;
            console.log(DataObject);
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
    var barChartData = {
        labels: ["Bahawalnagar",
            "Bahawalpur",
            "Rahim Yar Khan",
            "Dera Ghazi Khan",
            "Layyah",
            "Muzaffargarh",
            "Rajanpur",
            "Chiniot",
            "Faisalabad",
            "Jhang",
            "Toba Tek Singh",
            "Gujranwala",
            "Gujrat",
            "Hafizabad",
            "Mandi Bahauddin",
            "Narowal",
            "Sialkot",
            "Kasur",
            "Lahore",
            "Nankana Sahib",
            "Sheikhupura",
            "Khanewal",
            "Lodhran",
            "Multan",
            "Vehari",
            "Attock",
            "Chakwal",
            "Jhelum",
            "Rawalpindi",
            "Okara",
            "Pakpattan",
            "Sahiwal",
            "Bhakkar",
            "Khushab",
            "Mianwali",
            "Sargodha"],
        datasets: [{
            type: 'bar',
            label: 'Regular',
            backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
            borderColor: window.chartColors.red,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }, {
                type: 'bar',
            label: 'Addtional',
            backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
            borderColor: window.chartColors.blue,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
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
});

//Year Wise WaterCource Improvement Trend
$(document).ready(function () {
    $.ajax({
        url: '/HEIS/GetYearWiseWcImprStatus',
        type: "GET",
        dataType: "JSON",
        success: function (DataObject) {
            debugger;
            console.log(DataObject);
            var barChartData = {
                labels: ["1976-77",
                    "1977-78",
                    "1978-79",
                    "1979-80",
                    "1980-81",
                    "1981-82",
                    "1982-83",
                    "1983-84",
                    "1984-85",
                    "1985-86",
                    "1986-87",
                    "1987-88",
                    "1988-89",
                    "1989-90",
                    "1990-91",
                    "1991-92",
                    "1992-93",
                    "1993-94",
                    "1994-95",
                    "1995-96",
                    "1996-97",
                    "1997-98",
                    "1998-99",
                    "1999-00",
                    "2000-01",
                    "2001-02",
                    "2002-03",
                    "2003-04",
                    "2004-05",
                    "2005-06",
                    "2006-07",
                    "2007-08",
                    "2008-09",
                    "2009-10",
                    "2010-11",
                    "2011-12",
                    "2012-13",
                    "2013-14",
                    "2014-15",
                    "2015-16",
                    "2016-17",
                    "2017-18",
                    "2018-19",
                    "2019-20",
                    "2020-21"],
                datasets: [{
                    type: 'bar',
                    label: 'Watercourses',
                    backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
                    borderColor: window.chartColors.green,
                    data: [
                        6, 25, 145, 386, 564, 544, 867, 1255, 1050, 1277,
                        1196, 1538, 128, 1209, 1020, 1512, 1166, 1892, 1377, 1154,
                        468, 574, 685, 539, 314, 619, 845, 2346, 4398, 3962,
                        3889, 817, 1089, 2027, 403, 831, 1003, 1196, 555, 1651,
                        371, 131, 286, 314, 619
                    ]
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
    var color = Chart.helpers.color;

});

//districtWise_UnImprovementStatus
$(document).ready(function () {
    var randomScalingFactor = function () {
        return Math.round(Math.random() * 100);
    };

    var config = {
        type: 'pie',
        data: {
            datasets: [{
                data: [
                    18302,
                    18080
                ],
                backgroundColor: [
                    window.chartColors.red,
                    
                    window.chartColors.green,

                ],
                label: 'Status of 18,302 Watercourses'
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
});

//districtWise_WatercourseImprovementStatus
$(document).ready(function () {
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
                18302,
                68,
                154,
                18080
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
});

//districtWise_LengthOfImprovedWatercourses
$(document).ready(function () {
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
                1004454.00, 527526.90, 341314.70
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
});

//districtWise_TehsilWiseWatercourseImprovement
$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Bahawalnagar",
            "Bahawalpur",
            "Rahim Yar Khan",
            "Dera Ghazi Khan",
            "Layyah",
            "Muzaffargarh",
            "Rajanpur",
            "Chiniot",
            "Faisalabad",
            "Jhang",
            "Toba Tek Singh",
            "Gujranwala",
            "Gujrat",
            "Hafizabad",
            "Mandi Bahauddin",
            "Narowal",
            "Sialkot",
            "Kasur",
            "Lahore",
            "Nankana Sahib",
            "Sheikhupura",
            "Khanewal",
            "Lodhran",
            "Multan",
            "Vehari",
            "Attock",
            "Chakwal",
            "Jhelum",
            "Rawalpindi",
            "Okara",
            "Pakpattan",
            "Sahiwal",
            "Bhakkar",
            "Khushab",
            "Mianwali",
            "Sargodha"],
        datasets: [{
            type: 'bar',
            label: 'Regular',
            backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
            borderColor: window.chartColors.red,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }, {
            type: 'bar',
            label: 'Addtional',
            backgroundColor: color(window.chartColors.blue).alpha(0.2).rgbString(),
            borderColor: window.chartColors.blue,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
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
});