let pollBarChart;

window.drawBarChart = (poll, canvasRef) => {
    const context = canvasRef.getContext('2d');

    let labels = getLabelsFromPoll(poll);
    let counts = getCountsFromPoll(poll);
    let suggestedMax = getSuggestedMax(counts);

    pollBarChart = new Chart(context,
        {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [
                    {
                        label: '# of votes',
                        data: counts
                    }
                ]
            },
            options: {
                scales: {
                    yAxes: [
                        {
                            ticks: {
                                beginAtZero: true,
                                stepSize: 1,
                                suggestedMax: suggestedMax
                            }
                        }]
                },
                responsive: true,
                maintainAspectRatio: false
            }
        });
}

window.updateChartCounts = (poll) => {
    let newCounts = getCountsFromPoll(poll);

    // set counts
    pollBarChart.data.datasets.forEach(dataset => {
        dataset.data = newCounts;
    });

    // set suggested max
    pollBarChart.options.scales.yAxes[0].ticks.suggestedMax = getSuggestedMax(newCounts);

    pollBarChart.update();
}

function getLabelsFromPoll(poll) {
    let labels = [];

    poll.answers.forEach(a => labels.push(a.content));
    return labels;
}

function getCountsFromPoll(poll) {
    let counts = [];

    poll.answers.forEach(a => counts.push(a.count));
    return counts;
}

function getSuggestedMax(counts) {
    return Math.max(...counts) + 1;
}