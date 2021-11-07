var ctx = null;
var chart = null;
var chartWidth = 1300;
var chartHeight = 650;
var currentKlin = null;
var currentLineData = null;
var currentVolume = null;
var nextBar = false;
var inDay = false;
var timerId = null;
var resizeObserver = null;
var timerVisible = null;
var allPrices = null;
var allVolumes = null;


export function showAreaChart(data, element, dotnetHelper) {
    ctx = element;
    if (timerId != null) {
        clearInterval(timerId);
    }
    if (resizeObserver != null) {
        resizeObserver.unobserve(ctx);
    }
    inDay = data.inDay;
    ctx.innerHTML = '';
    nextBar = false;
    chart = null;

    chart = LightweightCharts.createChart(ctx, {
        width: chartWidth,
        height: chartHeight,
        layout: {
            backgroundColor: '#ffffff'
        },
        crosshair: {
            mode: LightweightCharts.CrosshairMode.Normal,
        },
        grid: {
            vertLines: {
                color: 'rgb(211,211,211, 0.5)'
            },
            horzLines: {
                color: 'rgb(211,211,211, 0.5)'
            }
        },
        rightPriceScale: {
            borderColor: 'rgb(211,211,211, 0.5)',
        },
        timeScale: {
            borderColor: 'rgba(197, 203, 206, 1)'
        }
    });

    if (data.inDay) {
        chart.applyOptions({
            timeScale: {
                borderColor: 'rgba(197, 203, 206, 1)',
                timeVisible: true,
                secondsVisible: false
            }
        });
    }

    beginObserveChartSize();

    var areaSeries = chart.addAreaSeries({
        topColor: 'rgba(33, 150, 243, 0.56)',
        bottomColor: 'rgba(33, 150, 243, 0.04)',
        lineColor: 'rgba(33, 150, 243, 1)',
        lineWidth: 2
    });
    var volumeSeries = chart.addHistogramSeries({
        color: '#26a69a',
        priceFormat: {
            type: 'volume',
        },
        priceScaleId: '',
        scaleMargins: {
            top: 0.9,
            bottom: 0,
        },
    });

    var lineData;
    var volumes;

    if (data.inDay) {
        lineData = getIntradayLineData(data.dataset);
        volumes = getIntradayVolumeData(data.dataset);
    }
    else {
        lineData = getLineData(data.dataset);
        volumes = getVolumeData(data.dataset);
    }

    areaSeries.setData(lineData);
    volumeSeries.setData(volumes);

    addChartLegend(ctx, data, areaSeries, volumeSeries, 'areaSeries');
    addGoToRealTimeBtn(ctx);

    var lastIndex = lineData.length - 1;
    currentLineData = lineData[lastIndex];
    currentVolume = volumes[lastIndex];

    timerId = setInterval(changeLineChartInLive, 500, areaSeries, volumeSeries, dotnetHelper);

    allPrices = lineData;
    allVolumes = volumes;
    var timeScale = chart.timeScale();
    timerVisible = null
    timeScale.subscribeVisibleLogicalRangeChange(() => {
        if (timerVisible !== null) {
            return;
        }
        timerVisible = setTimeout(getHistData, 1200, timeScale, areaSeries, volumeSeries, 'line', inDay, dotnetHelper)
    });
}

export function showLineChart(data, element, dotnetHelper) {
    ctx = element;
    if (timerId != null) {
        clearInterval(timerId);
    }
    if (resizeObserver != null) {
        resizeObserver.unobserve(ctx);
    }
    inDay = data.inDay;
    ctx.innerHTML = '';
    nextBar = false;
    chart = null;

    chart = LightweightCharts.createChart(ctx, {
        width: chartWidth,
        height: chartHeight,
        layout: {
            backgroundColor: '#ffffff'
        },
        crosshair: {
            mode: LightweightCharts.CrosshairMode.Normal,
        },
        grid: {
            vertLines: {
                color: 'rgb(211,211,211, 0.5)'
            },
            horzLines: {
                color: 'rgb(211,211,211, 0.5)'
            }
        },
        rightPriceScale: {
            borderColor: 'rgb(211,211,211, 0.5)',
        },
        timeScale: {
            borderColor: 'rgba(197, 203, 206, 1)'
        }
    });

    if (data.inDay) {
        chart.applyOptions({
            timeScale: {
                borderColor: 'rgba(197, 203, 206, 1)',
                timeVisible: true,
                secondsVisible: false
            }
        });
    }

    beginObserveChartSize();

    var lineSeries = chart.addLineSeries({
        lineColor: 'rgba(33, 150, 243, 1)',
        lineWidth: 2
    });
    var volumeSeries = chart.addHistogramSeries({
        color: '#26a69a',
        priceFormat: {
            type: 'volume',
        },
        priceScaleId: '',
        scaleMargins: {
            top: 0.9,
            bottom: 0,
        },
    });

    var lineData;
    var volumes;

    if (data.inDay) {
        lineData = getIntradayLineData(data.dataset);
        volumes = getIntradayVolumeData(data.dataset);
    }
    else {
        lineData = getLineData(data.dataset);
        volumes = getVolumeData(data.dataset);
    }

    lineSeries.setData(lineData);
    volumeSeries.setData(volumes);

    addChartLegend(ctx, data, lineSeries, volumeSeries, 'lineSeries');
    addGoToRealTimeBtn(ctx);

    var lastIndex = lineData.length - 1;
    currentLineData = lineData[lastIndex];
    currentVolume = volumes[lastIndex];

    timerId = setInterval(changeLineChartInLive, 500, lineSeries, volumeSeries, dotnetHelper);

    allPrices = lineData;
    allVolumes = volumes;
    var timeScale = chart.timeScale();
    timerVisible = null
    timeScale.subscribeVisibleLogicalRangeChange(() => {
        if (timerVisible !== null) {
            return;
        }
        timerVisible = setTimeout(getHistData, 1200, timeScale, lineSeries, volumeSeries, 'line', inDay, dotnetHelper)
    });
}

export function showCandleChart(data, element, dotnetHelper) {
    ctx = element;
    if (timerId != null) {
        clearInterval(timerId);
    }
    if (resizeObserver != null) {
        resizeObserver.unobserve(ctx);
    }

    ctx.innerHTML = '';
    nextBar = false;
    inDay = data.inDay;
    chart = null;

    chart = LightweightCharts.createChart(ctx, {
        width: chartWidth,
        height: chartHeight,
        layout: {
            backgroundColor: '#ffffff',
            textColor: '#000',
        },
        grid: {
            vertLines: {
                color: 'rgb(211,211,211, 0.5)',
            },
            horzLines: {
                color: 'rgb(211,211,211, 0.5)',
            },
        },
        crosshair: {
            mode: LightweightCharts.CrosshairMode.Normal,
        },
        rightPriceScale: {
            borderColor: 'rgb(211,211,211, 0.5)',
        },
        timeScale: {
            borderColor: 'rgba(197, 203, 206, 1)'
        }
    });

    if (data.inDay) {
        chart.applyOptions({
            timeScale: {
                borderColor: 'rgba(197, 203, 206, 1)',
                timeVisible: true,
                secondsVisible: false
            }
        });
    }

    beginObserveChartSize();

    var candleSeries = chart.addCandlestickSeries({
        upColor: 'rgb(38,166,154)',
        downColor: 'rgb(255,82,82)',
        wickUpColor: 'rgb(38,166,154)',
        wickDownColor: 'rgb(255,82,82)'
    });

    var volumeSeries = chart.addHistogramSeries({
        color: '#26a69a',
        priceFormat: {
            type: 'volume',
        },
        priceScaleId: '',
        scaleMargins: {
            top: 0.9,
            bottom: 0,
        },
    });

    var klins;
    var volumes;

    if (inDay) {
        klins = getIntradayKlineData(data.dataset);
        volumes = getIntradayVolumeData(data.dataset);
    }
    else {
        klins = getKlineData(data.dataset);
        volumes = getVolumeData(data.dataset);
    }

    candleSeries.setData(klins);
    volumeSeries.setData(volumes);

    addChartLegend(ctx, data, candleSeries, volumeSeries, 'candleSeries');
    addGoToRealTimeBtn(ctx);

    var lastIndex = klins.length - 1;
    currentKlin = klins[lastIndex];
    currentVolume = volumes[lastIndex];

    timerId = setInterval(changeKlinChartInLive, 500, candleSeries, volumeSeries, dotnetHelper);

    allPrices = klins;
    allVolumes = volumes;
    var timeScale = chart.timeScale();
    timerVisible = null
    timeScale.subscribeVisibleLogicalRangeChange(() => {
        if (timerVisible !== null) {
            return;
        }
        timerVisible = setTimeout(getHistData, 1200, timeScale, candleSeries, volumeSeries, 'klin', inDay, dotnetHelper)
    });
}

export function showBarChart(data, element, dotnetHelper) {
    ctx = element;
    if (timerId != null) {
        clearInterval(timerId);
    }
    if (resizeObserver != null) {
        resizeObserver.unobserve(ctx);
    }

    ctx.innerHTML = '';
    nextBar = false;
    inDay = data.inDay;
    chart = null;

    chart = LightweightCharts.createChart(ctx, {
        width: chartWidth,
        height: chartHeight,
        layout: {
            backgroundColor: '#ffffff',
            textColor: '#000',
        },
        crosshair: {
            mode: LightweightCharts.CrosshairMode.Normal,
        },
        grid: {
            vertLines: {
                color: 'rgb(211,211,211, 0.5)',
            },
            horzLines: {
                color: 'rgb(211,211,211, 0.5)',
            },
        },
        rightPriceScale: {
            borderColor: 'rgb(211,211,211, 0.5)',
        },
        timeScale: {
            borderColor: 'rgba(197, 203, 206, 1)'
        }
    });

    if (data.inDay) {
        chart.applyOptions({
            timeScale: {
                borderColor: 'rgba(197, 203, 206, 1)',
                timeVisible: true,
                secondsVisible: false
            }
        });
    }

    beginObserveChartSize();

    var barSeries = chart.addBarSeries({
        thinBars: false,
        upColor: 'rgb(38,166,154)',
        downColor: 'rgb(255,82,82)'
    });

    var volumeSeries = chart.addHistogramSeries({
        color: '#26a69a',
        priceFormat: {
            type: 'volume',
        },
        priceScaleId: '',
        scaleMargins: {
            top: 0.9,
            bottom: 0,
        },
    });

    var klins, volumes;

    if (inDay) {
        klins = getIntradayKlineData(data.dataset);
        volumes = getIntradayVolumeData(data.dataset);
    }
    else {
        klins = getKlineData(data.dataset);
        volumes = getVolumeData(data.dataset);
    }

    barSeries.setData(klins);
    volumeSeries.setData(volumes);

    addChartLegend(ctx, data, barSeries, volumeSeries, 'barSeries');
    addGoToRealTimeBtn(ctx);

    var lastIndex = klins.length - 1;
    currentKlin = klins[lastIndex];
    currentVolume = volumes[lastIndex];

    timerId = setInterval(changeKlinChartInLive, 500, barSeries, volumeSeries, dotnetHelper);

    allPrices = klins;
    allVolumes = volumes;
    var timeScale = chart.timeScale();
    timerVisible = null
    timeScale.subscribeVisibleLogicalRangeChange(() => {
        if (timerVisible !== null) {
            return;
        }
        timerVisible = setTimeout(getHistData, 1200, timeScale, barSeries, volumeSeries, 'klin', inDay, dotnetHelper)
    });
}
function getHistData(timeScale, dataSeries, volumeSeries, type, inDay, dotnetHelper) {
    var logicalRange = timeScale.getVisibleLogicalRange();
    if (logicalRange !== null) {
        var barsInfo = dataSeries.barsInLogicalRange(logicalRange);
        if (barsInfo !== null && barsInfo.barsBefore < 10) {
            var oldData;
            dotnetHelper.invokeMethodAsync('GetOldData')
                .then(histKlins => { oldData = histKlins })
                .then(() => {
                    if (oldData !== null) {
                        var prices, volumes;
                        if (type === 'klin') {
                            if (inDay) {
                                prices = getIntradayKlineData(oldData);
                                volumes = getIntradayVolumeData(oldData);
                            }
                            else {
                                prices = getKlineData(oldData);
                                volumes = getVolumeData(oldData);
                            }
                        }
                        else {
                            if (inDay) {
                                prices = getIntradayLineData(oldData);
                                volumes = getIntradayVolumeData(oldData);
                            }
                            else {
                                prices = getLineData(oldData);
                                volumes = getVolumeData(oldData);
                            }
                        }
                        allPrices = [...prices, ...allPrices];
                        allVolumes = [...volumes, ...allVolumes];
                        dataSeries.setData(allPrices);
                        volumeSeries.setData(allVolumes)
                    }
                });
        }
    }
    timerVisible = null;
}

function changeKlinChartInLive(dataSeries, volumeSeries, dotnetHelper) {
    var lastKlin;
    dotnetHelper.invokeMethodAsync('UpdateKlin')
        .then(klin => { lastKlin = klin })
        .then(() => {
            if (lastKlin.final !== undefined) {
                if (nextBar && lastKlin.final !== true) {
                    if (inDay) {
                        // move to next bar
                        currentKlin = {
                            open: null,
                            high: null,
                            low: null,
                            close: null,
                            time: Date.parse(lastKlin.closeTime).valueOf() / 1000
                        };
                        currentVolume = {
                            value: null,
                            time: Date.parse(lastKlin.closeTime).valueOf() / 1000,
                            color: 'rgba(0, 150, 136, 0.7)'
                        };
                    }
                    else {
                        currentKlin = {
                            open: null,
                            high: null,
                            low: null,
                            close: null,
                            time: lastKlin.closeTime
                        };
                        currentVolume = {
                            value: null,
                            time: lastKlin.closeTime,
                            color: 'rgba(0, 150, 136, 0.7)'
                        };
                    }
                    nextBar = false;
                }
                mergeTickToKlin(lastKlin, dataSeries, volumeSeries);
                if (lastKlin.final === true) {
                    nextBar = true;
                }
            }
        });
}

function mergeTickToKlin(klin, dataSeries, volumeSeries) {
    if (currentKlin.open === null) {
        currentKlin.open = klin.open;
        currentKlin.high = klin.high;
        currentKlin.low = klin.low;
        currentKlin.close = klin.close;
    } else {
        currentKlin.close = klin.close;
        currentKlin.high = klin.high;
        currentKlin.low = klin.low;
    }

    currentVolume.value = klin.baseVolume;

    dataSeries.update(currentKlin);
    volumeSeries.update(currentVolume);
}

function changeLineChartInLive(dataSeries, volumeSeries, dotnetHelper) {
    var lastLineData;
    dotnetHelper.invokeMethodAsync('UpdateKlin')
        .then(lineData => { lastLineData = lineData })
        .then(() => {
            if (lastLineData.final !== undefined) {
                if (nextBar && lastLineData.final !== true) {
                    if (inDay) {
                        // move to next bar
                        currentLineData = {
                            value: null,
                            time: Date.parse(lastLineData.closeTime).valueOf() / 1000
                        };
                        currentVolume = {
                            value: null,
                            time: Date.parse(lastLineData.closeTime).valueOf() / 1000,
                            color: 'rgba(0, 150, 136, 0.7)'
                        };
                    }
                    else {
                        currentLineData  = {
                            value: null,
                            time: lastLineData.closeTime
                        };
                        currentVolume = {
                            value: null,
                            time: lastLineData.closeTime,
                            color: 'rgba(0, 150, 136, 0.7)'
                        };
                    }
                    nextBar = false;
                    //mergeTickToLine(lastLineData, dataSeries, volumeSeries);
                }
                mergeTickToLine(lastLineData, dataSeries, volumeSeries);
                if (lastLineData.final === true) {
                    nextBar = true;
                }
            }
        });
}

function mergeTickToLine(lineData, dataSeries, volumeSeries) {

    currentLineData.value = lineData.close;
    currentVolume.value = lineData.baseVolume;

    dataSeries.update(currentLineData);
    volumeSeries.update(currentVolume);
}

function getLineData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            time: dataset[i].closeTime,
            value: dataset[i].close
        })
    }
    return data;
}

function getIntradayLineData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            time: Date.parse(dataset[i].closeTime).valueOf() / 1000,
            value: dataset[i].close
        })
    }
    return data;
}

function getKlineData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            open: dataset[i].open,
            close: dataset[i].close,
            high: dataset[i].high,
            low: dataset[i].low,
            time: dataset[i].closeTime
        })
    }
    return data;
}

function getIntradayKlineData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            open: dataset[i].open,
            close: dataset[i].close,
            high: dataset[i].high,
            low: dataset[i].low,
            time: Date.parse(dataset[i].closeTime).valueOf() / 1000
        })
    }
    return data;
}

function getVolumeData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            value: dataset[i].baseVolume,
            time: dataset[i].closeTime,
            color: 'rgba(0, 150, 136, 0.7)'
        })
    }
    return data;
}

function getIntradayVolumeData(dataset) {
    var data = [];
    for (var i = 0; i < dataset.length; i++) {
        data.push({
            time: Date.parse(dataset[i].closeTime).valueOf() / 1000,
            value: dataset[i].baseVolume,
            color: 'rgba(0, 150, 136, 0.7)'
        })
    }
    return data;
}

function beginObserveChartSize() {
    resizeObserver = new ResizeObserver(entries => {
        if (entries.length === 0 || entries[0].target !== ctx) {
            return
        }
        var newRect = entries[0].contentRect;
        if (newRect.width != 0 && newRect.height != 0) {
            chart.applyOptions({
                height: newRect.height,
                width: newRect.width
            });
            chartWidth = newRect.width;
            chartHeight = newRect.height;
            document.getElementById('real-time-btn').style.left = (chartWidth - 27 - 70) + 'px';
        }
    });
    resizeObserver.observe(ctx);
}

function addChartLegend(ctx, info, priceSeries, volumeSeries, type) {
    var legend = document.createElement('div');
    legend.classList.add('chart-legend');
    ctx.prepend(legend);

    var firstRow = document.createElement('div');
    firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange;
    firstRow.style.color = 'black';

    var secondRow = document.createElement('div');
    secondRow.innerText = 'Vol';

    legend.appendChild(firstRow);
    legend.appendChild(secondRow);

    chart.subscribeCrosshairMove((param) => {
        if (param.time) {
            const price = param.seriesPrices.get(priceSeries);
            const volume = param.seriesPrices.get(volumeSeries);

            if (type === 'areaSeries' || type === 'lineSeries') {
                if (price !== undefined) {
                    firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange + ' ' + price.toFixed(2);
                }
                else {
                    firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange;
                }
            }
            else {
                if (price.open !== undefined) {
                    firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange + ' ' + 'O ' + price.open.toFixed(2) + '  ' + 'C ' + price.close.toFixed(2) + '  ' + 'H ' + price.high.toFixed(2) + '  ' + 'L ' + price.low.toFixed(2);
                }
                else {
                    firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange;
                }
            }
            if (volume !== undefined) {
                secondRow.innerText = 'Vol ' + volume.toFixed(2);
            }

        }
        else {
            firstRow.innerText = info.pair + '  ' + info.interval + ' ' + info.exchange;
            secondRow.innerText = 'Vol';
        }
    });
}

function addGoToRealTimeBtn(ctx) {
    chart.timeScale().scrollToPosition(0, false);

    var width = 27;
    var height = 27;

    var button = document.createElement('div');
    button.classList.add('go-to-realtime-button');
    button.style.left = (chartWidth - width - 70) + 'px';
    button.style.top = (chartHeight - height - 35) + 'px';
    button.style.color = '#4c525e';
    button.id = 'real-time-btn';
    button.innerHTML = '<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 14 14" width="14" height="14"><path fill="none" stroke="currentColor" stroke-linecap="round" stroke-width="2" d="M6.5 1.5l5 5.5-5 5.5M3 4l2.5 3L3 10"></path></svg>';
    ctx.appendChild(button);

    var timeScale = chart.timeScale();
    timeScale.subscribeVisibleTimeRangeChange(function () {
        var buttonVisible = timeScale.scrollPosition() < 0;
        button.style.display = buttonVisible ? 'block' : 'none';
    });

    button.addEventListener('click', function () {
        timeScale.scrollToRealTime();
    });

    button.addEventListener('mouseover', function () {
        button.style.background = 'rgba(250, 250, 250, 1)';
        button.style.color = '#000';
    });

    button.addEventListener('mouseout', function () {
        button.style.background = 'rgba(250, 250, 250, 0.6)';
        button.style.color = '#4c525e';
    });
}

export function stopTimerHandle() {
    if (timerId != null) {
        clearInterval(timerId);
    }
    if (timerVisible != null) {
        clearTimeout(timerVisible);
    }
};