
$(document).ready(function() {
	var randomScalingFactor = function() {
        return Math.round(Math.random() * 100);
    };

    var config = {
        type: 'pie',
    data: {
        datasets: [{
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                
            ],
            backgroundColor: [
                window.chartColors.red,
                window.chartColors.orange,
                window.chartColors.yellow,
                window.chartColors.green,
                
            ],
            label: 'Status of 58,879 Watercourses'
        }],
        labels: [
            "Total",
            "Regular",
            "Additional",
            "Un-improved",
           
        ]
    },
    options: {
        responsive: true
    }
};
    var ctx = document.getElementById("chartjs_pie").getContext("2d");
    window.myPie = new Chart(ctx, config);
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

$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Total", "Regular", "Additional", "Un-improved"],
        datasets: [{
            type: 'bar',
            label: 'Watercourse Improvement Status',
            backgroundColor: color(window.chartColors.red).alpha(0.2).rgbString(),
            borderColor: window.chartColors.red,
            data: [
                58879,
                49531,
                6793,
                9348
            ]
        } ]
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
});

$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Total Length", "Earthen", "Lined"],
        datasets: [{
            type: 'bar',
            label: 'Length of Improved Watercourses (000 KMs)',
            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
            borderColor: window.chartColors.green,
            data: [
                255.169, 175.881, 49.288
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
});

$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Total Cost", "Govt. Assistance", "Farmer Contribution"],
        datasets: [{
            type: 'bar',
            label: 'Financial Implication (Rs. Billion)',
            backgroundColor: color(window.chartColors.green).alpha(0.2).rgbString(),
            borderColor: window.chartColors.green,
            data: [
                58.314, 37.428, 20.886
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
});


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


$(document).ready(function () {
    var color = Chart.helpers.color;
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
});


$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Total", "Regular", "Additional", "Un-Improved"],
        datasets: [{
            label: 'Improved',
            backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
            borderColor: window.chartColors.red,
            borderWidth: 1,
            data: [

                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }]

    };

    var ctx = document.getElementById("districtWise_WatercourseImprovementStatus").getContext("2d");
    window.myBar = new Chart(ctx, {
        type: 'bar',
        data: barChartData,
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Watercourse Improvement Status'
            }
        }
    });

});

$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Total Length", "Earthen", "Lined"],
        datasets: [{
            
            backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
            borderColor: window.chartColors.red,
            borderWidth: 1,
            data: [
                randomScalingFactor(),
                randomScalingFactor(),
                randomScalingFactor()
            ]
        }]

    };

    var ctx = document.getElementById("districtWise_LengthOfImprovedWatercourses").getContext("2d");
    window.myBar = new Chart(ctx, {
        type: 'bar',
        data: barChartData,
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Length of Improved Watercourses ( KMs)'
            }
        }
    });

});


$(document).ready(function () {
    var color = Chart.helpers.color;
    var barChartData = {
        labels: ["Bahawalnagar", "Chishtian", "Fortabbas", "Haroonabad", "Minchinabad", "Tehsil", "Fortabbas1"],
        datasets: [{
            label: 'Improved',
            backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
            borderColor: window.chartColors.red,
            borderWidth: 1,
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
                label: 'Un-Improved',
            backgroundColor: color(window.chartColors.blue).alpha(0.5).rgbString(),
            borderColor: window.chartColors.blue,
            borderWidth: 1,
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

    var ctx = document.getElementById("districtWise_TehsilWiseWatercourseImprovement").getContext("2d");
    window.myBar = new Chart(ctx, {
        type: 'bar',
        data: barChartData,
        options: {
            responsive: true,
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Bar Chart'
            }
        }
    });

});