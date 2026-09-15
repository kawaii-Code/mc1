"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
class NumberRange {
    min;
    max;
    constructor(min = 0, max = 1) {
        console.assert(min <= max);
        this.min = min;
        this.max = max;
    }
}
var IntegrationMethod;
(function (IntegrationMethod) {
    IntegrationMethod[IntegrationMethod["LeftRiemann"] = 1] = "LeftRiemann";
    IntegrationMethod[IntegrationMethod["RightRiemann"] = 2] = "RightRiemann";
    IntegrationMethod[IntegrationMethod["Midpoint"] = 3] = "Midpoint";
    IntegrationMethod[IntegrationMethod["Trapezoidal"] = 4] = "Trapezoidal";
})(IntegrationMethod || (IntegrationMethod = {}));
;
const INTEGRATION_POINTS = 10;
function leftRiemannSum(fn, range) {
    let result = 0.0;
    const step = (range.max - range.min) / INTEGRATION_POINTS;
    let x = range.min;
    for (let i = 0; i < INTEGRATION_POINTS; i++) {
        const y = fn(x);
        result += Math.abs(y) * step;
        x += step;
    }
    return result;
}
function rightRiemannSum(fn, range) {
    let result = 0.0;
    const step = (range.max - range.min) / INTEGRATION_POINTS;
    let x = range.min;
    for (let i = 0; i < INTEGRATION_POINTS; i++) {
        const y = fn(x + step);
        result += Math.abs(y) * step;
        x += step;
    }
    return result;
}
function midpointRiemannSum(fn, range) {
    let result = 0.0;
    const step = (range.max - range.min) / INTEGRATION_POINTS;
    let x = range.min;
    for (let i = 0; i < INTEGRATION_POINTS; i++) {
        const y = fn(x + 0.5 * step);
        result += Math.abs(y) * step;
        x += step;
    }
    return result;
}
function trapezoidalRiemannSum(fn, range) {
    let result = 0.0;
    const step = (range.max - range.min) / INTEGRATION_POINTS;
    let x = range.min;
    for (let i = 0; i < INTEGRATION_POINTS; i++) {
        const y1 = fn(x);
        const y2 = fn(x + step);
        result += Math.abs((y1 + y2) / 2) * step;
        x += step;
    }
    return result;
}
function integrateWithMethod(fn, range, method) {
    switch (method) {
        case IntegrationMethod.LeftRiemann: return leftRiemannSum(fn, range);
        case IntegrationMethod.RightRiemann: return rightRiemannSum(fn, range);
        case IntegrationMethod.Midpoint: return midpointRiemannSum(fn, range);
        case IntegrationMethod.Trapezoidal: return trapezoidalRiemannSum(fn, range);
    }
    // Есть unreachable?
    console.assert(false);
    return 0.0;
}
function integrate(fn, range) {
    // Как проитерировать по значениям enum-а?
    console.log("IntegrationMethod.LeftRiemann", integrateWithMethod(fn, range, IntegrationMethod.LeftRiemann));
    console.log("IntegrationMethod.RightRiemann", integrateWithMethod(fn, range, IntegrationMethod.RightRiemann));
    console.log("IntegrationMethod.Midpoint", integrateWithMethod(fn, range, IntegrationMethod.Midpoint));
    console.log("IntegrationMethod.Trapezoidal", integrateWithMethod(fn, range, IntegrationMethod.Trapezoidal));
}
const functions = [
    (x) => x * x - Math.cos(x),
    (x) => x * x * x + Math.sin(x),
    (x) => x * x - x + 1,
    (x) => x * x - x + 1,
    (x) => x * Math.cos(x),
    (x) => 4 * x - x * x,
    (x) => x + x * x - Math.pow(x, 3),
];
const min = prompt("Нижняя граница 👇", "0");
const max = prompt("Верхняя граница ☝", "1");
if (min === null || max === null) {
    alert("Так нельзя! Перезагрузи страницу и нормально введи числа, гад!");
}
const range = new NumberRange(+min, +max);
console.log(`Integration points: ${INTEGRATION_POINTS}`);
console.log(`Range: ${range.min}...${range.max}`);
for (const fn of functions) {
    console.log("==============================");
    integrate(fn, range);
}
//# sourceMappingURL=index.js.map