webpackJsonp(["main"],{

/***/ "../../../../../src/$$_gendir lazy recursive":
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncatched exception popping up in devtools
	return Promise.resolve().then(function() {
		throw new Error("Cannot find module '" + req + "'.");
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "../../../../../src/$$_gendir lazy recursive";

/***/ }),

/***/ "../../../../../src/app/app.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, ".selected {\r\n    background-color: #CFD8DC !important;\r\n    color: white;\r\n  }\r\n  .geocontainers {\r\n    margin: 0 0 2em 0;\r\n    list-style-type: none;\r\n    padding: 0;\r\n    width: 36em;\r\n  }\r\n  .geocontainers li {\r\n    cursor: pointer;\r\n    position: relative;\r\n    left: 0;\r\n    background-color: #EEE;\r\n    margin: .5em;\r\n    padding: .3em 0;\r\n    height: 1.6em;\r\n    border-radius: 4px;\r\n  }\r\n  .geocontainers li.selected:hover {\r\n    background-color: #BBD8DC !important;\r\n    color: white;\r\n  }\r\n  .geocontainers li:hover {\r\n    color: #607D8B;\r\n    background-color: #DDD;\r\n    left: .1em;\r\n}\r\n.geocontainers .text {\r\n  position: relative;\r\n  top: -3px;\r\n}\r\n.geocontainers .badge {\r\n  display: inline-block;\r\n  font-size: small;\r\n  color: white;\r\n  padding: 0.8em 0.7em 0 0.7em;\r\n  background-color: #607D8B;\r\n  line-height: 1em;\r\n  position: relative;\r\n  left: -1px;\r\n  top: -4px;\r\n  height: 1.8em;\r\n  margin-right: .8em;\r\n  border-radius: 4px 0 0 4px;\r\n}\r\n", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/app.component.html":
/***/ (function(module, exports) {

module.exports = "<ul class=\"geocontainers\">\n  <li *ngFor=\"let cont of layoutContainers\" \n  [class.selected]=\"cont === selectedContainer\"\n  (click)=\"onSelect(cont)\">{{cont.name}}</li>\n</ul>\n<hr />\n<app-geo-square [container]=\"selectedContainer\"></app-geo-square>\n\n\n\n\n\n\n\n"

/***/ }),

/***/ "../../../../../src/app/app.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Rx__ = __webpack_require__("../../../../rxjs/_esm5/Rx.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__ = __webpack_require__("../../../../../src/app/geo-layout-accessor.service.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AppComponent = (function () {
    function AppComponent(layoutSvc) {
        this.layoutSvc = layoutSvc;
        this.title = 'Geomertical Layout Client';
    }
    AppComponent.prototype.ngOnInit = function () {
        var _this = this;
        var subscr = this.layoutSvc.getLayoutContainers()
            .subscribe(function (data) {
            _this.layoutContainers = data;
            if (data.length > 0)
                _this.onSelect(data[0]);
        }, function (Error) { _this.layoutContainers = []; console.error(); });
    };
    AppComponent.prototype.onSelect = function (container) {
        this.readSegments(container);
        this.readTriangles(container);
    };
    ;
    AppComponent.prototype.readTriangles = function (value) {
        var _this = this;
        if (!value.triangles || value.triangles.length === 0) {
            var asyTriangles = this.layoutSvc.getContainerTriangles(value.index)
                .subscribe(function (data) { value.triangles = data; _this.selectedContainer = value; }, function (Error) { value.triangles = []; });
        }
        else
            this.selectedContainer = value;
    };
    AppComponent.prototype.readSegments = function (value) {
        if (!value.gridlines || value.gridlines.length === 0) {
            var asySegments = this.layoutSvc.getContainerGridSegments(value.index)
                .subscribe(function (data) { value.gridlines = data; }, function (Error) { value.gridlines = []; });
        }
    };
    return AppComponent;
}());
AppComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'app-root',
        template: __webpack_require__("../../../../../src/app/app.component.html"),
        styles: [__webpack_require__("../../../../../src/app/app.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */]) === "function" && _a || Object])
], AppComponent);

var _a;
//# sourceMappingURL=app.component.js.map

/***/ }),

/***/ "../../../../../src/app/app.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return AppModule; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__ = __webpack_require__("../../../platform-browser/@angular/platform-browser.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__angular_common_http__ = __webpack_require__("../../../common/@angular/common/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__app_component__ = __webpack_require__("../../../../../src/app/app.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_4__geo_layout_accessor_service__ = __webpack_require__("../../../../../src/app/geo-layout-accessor.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_5__geo_square_geo_square_component__ = __webpack_require__("../../../../../src/app/geo-square/geo-square.component.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_6__triangle_selector_triangle_selector_component__ = __webpack_require__("../../../../../src/app/triangle-selector/triangle-selector.component.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_1__angular_core__["L" /* NgModule */])({
        declarations: [
            __WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* AppComponent */],
            __WEBPACK_IMPORTED_MODULE_5__geo_square_geo_square_component__["a" /* GeoSquareComponent */],
            __WEBPACK_IMPORTED_MODULE_6__triangle_selector_triangle_selector_component__["a" /* TriangleSelectorComponent */]
        ],
        imports: [
            __WEBPACK_IMPORTED_MODULE_0__angular_platform_browser__["a" /* BrowserModule */],
            __WEBPACK_IMPORTED_MODULE_2__angular_common_http__["b" /* HttpClientModule */]
        ],
        providers: [__WEBPACK_IMPORTED_MODULE_2__angular_common_http__["a" /* HttpClient */], __WEBPACK_IMPORTED_MODULE_4__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */]],
        bootstrap: [__WEBPACK_IMPORTED_MODULE_3__app_component__["a" /* AppComponent */]]
    })
], AppModule);

//# sourceMappingURL=app.module.js.map

/***/ }),

/***/ "../../../../../src/app/geo-container/geo-container.module.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GeoContainer; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "b", function() { return TriangleInfo; });
/* unused harmony export GridSegment */
/* unused harmony export TriangleLocation */
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common__ = __webpack_require__("../../../common/@angular/common.es5.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var GeoContainer = (function () {
    function GeoContainer() {
        this.index = 0;
        this.triangles = [];
        this.gridlines = [];
    }
    return GeoContainer;
}());
GeoContainer = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["L" /* NgModule */])({
        imports: [
            __WEBPACK_IMPORTED_MODULE_1__angular_common__["a" /* CommonModule */]
        ],
        declarations: []
    })
], GeoContainer);

var TriangleInfo = (function () {
    function TriangleInfo() {
        this.containerIdx = 0;
    }
    return TriangleInfo;
}());

var GridSegment = (function () {
    function GridSegment() {
    }
    return GridSegment;
}());

var TriangleLocation = (function () {
    function TriangleLocation() {
        this.row = 0;
        this.column = 0;
        this.address = 0;
    }
    return TriangleLocation;
}());

//# sourceMappingURL=geo-container.module.js.map

/***/ }),

/***/ "../../../../../src/app/geo-layout-accessor.service.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GeoLayoutAccessorService; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_common_http__ = __webpack_require__("../../../common/@angular/common/http.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2_rxjs_observable_of__ = __webpack_require__("../../../../rxjs/_esm5/observable/of.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3_rxjs_operators__ = __webpack_require__("../../../../rxjs/_esm5/operators.js");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var GeoLayoutAccessorService = (function () {
    function GeoLayoutAccessorService(http) {
        this.http = http;
        this.geoLayoutBaseUrl = 'http://localhost:63956/api';
    }
    Object.defineProperty(GeoLayoutAccessorService.prototype, "ContainersUrl", {
        get: function () {
            return this.geoLayoutBaseUrl + '/containers';
        },
        enumerable: true,
        configurable: true
    });
    GeoLayoutAccessorService.prototype.getContainersUrl = function (containerIdx) {
        return this.ContainersUrl + '/' + containerIdx;
    };
    GeoLayoutAccessorService.prototype.getGridSegmentsUrl = function (containerIdx) {
        return this.geoLayoutBaseUrl + '/gridsegments/' + containerIdx;
    };
    GeoLayoutAccessorService.prototype.getTrianglesUrl = function (containerIdx) {
        return this.geoLayoutBaseUrl + '/triangles/' + containerIdx;
    };
    GeoLayoutAccessorService.prototype.getTriangleAddressUrl = function (containerIdx, address) {
        return this.getTrianglesUrl(containerIdx) + '/' + address;
    };
    GeoLayoutAccessorService.prototype.getLocationUrl = function (triangle) {
        return this.geoLayoutBaseUrl + '/locations/' +
            triangle.containerIdx + '/triangle/' + triangle.ax + '/' +
            triangle.ay + '/' + triangle.bx + '/' + triangle.by + '/' +
            triangle.cx + '/' + triangle.cy;
    };
    GeoLayoutAccessorService.prototype.getLayoutContainers = function () {
        return this.http.get(this.ContainersUrl)
            .pipe(Object(__WEBPACK_IMPORTED_MODULE_3_rxjs_operators__["catchError"])(this.handleError('getGeoContainers', [])));
    };
    GeoLayoutAccessorService.prototype.getContainerGridSegments = function (containerIdx) {
        return this.http.get(this.getGridSegmentsUrl(containerIdx))
            .pipe(Object(__WEBPACK_IMPORTED_MODULE_3_rxjs_operators__["catchError"])(this.handleError('getContainerGridSegments', [])));
    };
    GeoLayoutAccessorService.prototype.getContainerTriangles = function (containerIdx) {
        return this.http.get(this.getTrianglesUrl(containerIdx))
            .pipe(Object(__WEBPACK_IMPORTED_MODULE_3_rxjs_operators__["catchError"])(this.handleError('getContainerTriangles', [])));
    };
    GeoLayoutAccessorService.prototype.getTriangleAtAddress = function (containerIdx, address) {
        return this.http.get(this.getTriangleAddressUrl(containerIdx, address))
            .pipe(Object(__WEBPACK_IMPORTED_MODULE_3_rxjs_operators__["catchError"])(this.handleError('getTriangleAtAddress', null)));
    };
    GeoLayoutAccessorService.prototype.getTriangleLocation = function (triangle) {
        return this.http.get(this.getLocationUrl(triangle))
            .pipe(Object(__WEBPACK_IMPORTED_MODULE_3_rxjs_operators__["catchError"])(this.handleError('getTriangleLocation', null)));
    };
    GeoLayoutAccessorService.prototype.handleError = function (operation, result) {
        if (operation === void 0) { operation = 'operation'; }
        return function (error) {
            console.error('An error occurred', error);
            return Object(__WEBPACK_IMPORTED_MODULE_2_rxjs_observable_of__["a" /* of */])(result);
        };
    };
    return GeoLayoutAccessorService;
}());
GeoLayoutAccessorService = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["B" /* Injectable */])(),
    __metadata("design:paramtypes", [typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_1__angular_common_http__["a" /* HttpClient */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__angular_common_http__["a" /* HttpClient */]) === "function" && _a || Object])
], GeoLayoutAccessorService);

var _a;
//# sourceMappingURL=geo-layout-accessor.service.js.map

/***/ }),

/***/ "../../../../../src/app/geo-square/geo-square.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "svg > poligon:hover { fill: #5214e0; }", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/geo-square/geo-square.component.html":
/***/ (function(module, exports) {

module.exports = "\n<table>\n  <tbody>\n    <tr>\n      <td>\n          <div *ngIf=\"triangles.length > 0\">  \n              <svg attr.height=\"{{container.height}}\" attr.width=\"{{container.width}}\">\n                      <polygon *ngFor=\"let t of triangles;\" \n                      attr.points=\"{{t.ax}},{{t.ay}} {{t.bx}},{{t.by}} {{t.cx}},{{t.cy}}\"\n                      style=\"stroke: #54523F; stroke-width:1; fill:#EBE3C5\" \n                      (click)=\"onSelectTriangle(t)\"/>\n              </svg>            \n          </div>\n          <label for=\"address\">Current triangle:</label><strong id=\"address\">{{currentTriangle.address}}</strong>\n      </td>\n      <td>\n          <app-triangle-selector [gridLines]=\"segments\" [container]=\"container\" [triangle]=\"currentTriangle\"></app-triangle-selector>\n      </td>\n    </tr>\n  </tbody>\n</table>\n\n\n\n\n\n\n\n\n\n\n"

/***/ }),

/***/ "../../../../../src/app/geo-square/geo-square.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return GeoSquareComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__geo_layout_accessor_service__ = __webpack_require__("../../../../../src/app/geo-layout-accessor.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__geo_container_geo_container_module__ = __webpack_require__("../../../../../src/app/geo-container/geo-container.module.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

//import 'rxjs/Rx';


var GeoSquareComponent = (function () {
    function GeoSquareComponent(layoutSvc) {
        this.layoutSvc = layoutSvc;
        this.segments = [];
        this.triangles = [];
    }
    Object.defineProperty(GeoSquareComponent.prototype, "container", {
        get: function () { return this._cont; },
        //asySegments: Observable<GridSegment[]>;
        //asyTriangles: Observable<TriangleInfo[]>;
        set: function (value) {
            this.triangles = value.triangles;
            this.segments = value.gridlines;
            //this.readSegments(value);
            this._cont = value;
        },
        enumerable: true,
        configurable: true
    });
    GeoSquareComponent.prototype.onSelectTriangle = function (value) {
        this.currentTriangle = value;
    };
    GeoSquareComponent.prototype.ngOnInit = function () {
    };
    GeoSquareComponent.prototype.ngOnDestroy = function () {
        //this.asySegments.unsubscribe();
    };
    return GeoSquareComponent;
}());
__decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Input */])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_2__geo_container_geo_container_module__["a" /* GeoContainer */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__geo_container_geo_container_module__["a" /* GeoContainer */]) === "function" && _a || Object),
    __metadata("design:paramtypes", [typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_2__geo_container_geo_container_module__["a" /* GeoContainer */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__geo_container_geo_container_module__["a" /* GeoContainer */]) === "function" && _b || Object])
], GeoSquareComponent.prototype, "container", null);
GeoSquareComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'app-geo-square',
        template: __webpack_require__("../../../../../src/app/geo-square/geo-square.component.html"),
        styles: [__webpack_require__("../../../../../src/app/geo-square/geo-square.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_1__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_1__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */]) === "function" && _c || Object])
], GeoSquareComponent);

var _a, _b, _c;
//# sourceMappingURL=geo-square.component.js.map

/***/ }),

/***/ "../../../../../src/app/triangle-selector/triangle-selector.component.css":
/***/ (function(module, exports, __webpack_require__) {

exports = module.exports = __webpack_require__("../../../../css-loader/lib/css-base.js")(false);
// imports


// module
exports.push([module.i, "", ""]);

// exports


/*** EXPORTS FROM exports-loader ***/
module.exports = module.exports.toString();

/***/ }),

/***/ "../../../../../src/app/triangle-selector/triangle-selector.component.html":
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"gridlines.length > 0\">  \n    <svg attr.height=\"{{container.height}}\" attr.width=\"{{container.width}}\">\n            <line *ngFor=\"let g of gridlines;\"\n            attr.x1=\"g.ax\" attr.x2=\"g.ay\" attr.y1=\"g.bx\" attr.y2=\"g.by\"            \n            style=\"stroke: #000000; stroke-width:1\" />\n            <polygon *ngIf=\"triangle\" \n                      attr.points=\"{{triangle.ax}},{{triangle.ay}} {{triangle.bx}},{{triangle.by}} {{triangle.cx}},{{triangle.cy}}\"\n                      style=\"stroke: rgb(32, 3, 136); stroke-width:1; fill:rgb(32, 3, 136)\"\n                      (click)=\"onGetAddress()\"/>\n    </svg>            \n</div>\n<label for=\"location\">Location:</label><strong id=\"location\">{{location.address}} ({{location.row}}, {{location.column}}) </strong>\n"

/***/ }),

/***/ "../../../../../src/app/triangle-selector/triangle-selector.component.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return TriangleSelectorComponent; });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1_rxjs_Rx__ = __webpack_require__("../../../../rxjs/_esm5/Rx.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__ = __webpack_require__("../../../../../src/app/geo-layout-accessor.service.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__geo_container_geo_container_module__ = __webpack_require__("../../../../../src/app/geo-container/geo-container.module.ts");
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var TriangleSelectorComponent = (function () {
    function TriangleSelectorComponent(layoutSvc) {
        this.layoutSvc = layoutSvc;
    }
    Object.defineProperty(TriangleSelectorComponent.prototype, "gridlines", {
        get: function () {
            if (this._grdlines)
                return this._grdlines;
            else if (this.container)
                return this.container.gridlines;
            else
                return [];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(TriangleSelectorComponent.prototype, "gridLines", {
        set: function (value) {
            this._grdlines = value;
        },
        enumerable: true,
        configurable: true
    });
    TriangleSelectorComponent.prototype.onGetAddress = function () {
        var _this = this;
        if (this.container && this.triangle) {
            var asyTr = this.layoutSvc.getTriangleLocation(this.triangle)
                .subscribe(function (data) { _this.location = data; }, function (Error) { location = null; });
        }
    };
    TriangleSelectorComponent.prototype.ngOnInit = function () {
    };
    return TriangleSelectorComponent;
}());
__decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Input */])(),
    __metadata("design:type", typeof (_a = typeof __WEBPACK_IMPORTED_MODULE_3__geo_container_geo_container_module__["b" /* TriangleInfo */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__geo_container_geo_container_module__["b" /* TriangleInfo */]) === "function" && _a || Object)
], TriangleSelectorComponent.prototype, "triangle", void 0);
__decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Input */])(),
    __metadata("design:type", typeof (_b = typeof __WEBPACK_IMPORTED_MODULE_3__geo_container_geo_container_module__["a" /* GeoContainer */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_3__geo_container_geo_container_module__["a" /* GeoContainer */]) === "function" && _b || Object)
], TriangleSelectorComponent.prototype, "container", void 0);
__decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["E" /* Input */])(),
    __metadata("design:type", Array),
    __metadata("design:paramtypes", [Array])
], TriangleSelectorComponent.prototype, "gridLines", null);
TriangleSelectorComponent = __decorate([
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["n" /* Component */])({
        selector: 'app-triangle-selector',
        template: __webpack_require__("../../../../../src/app/triangle-selector/triangle-selector.component.html"),
        styles: [__webpack_require__("../../../../../src/app/triangle-selector/triangle-selector.component.css")]
    }),
    __metadata("design:paramtypes", [typeof (_c = typeof __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */] !== "undefined" && __WEBPACK_IMPORTED_MODULE_2__geo_layout_accessor_service__["a" /* GeoLayoutAccessorService */]) === "function" && _c || Object])
], TriangleSelectorComponent);

var _a, _b, _c;
//# sourceMappingURL=triangle-selector.component.js.map

/***/ }),

/***/ "../../../../../src/environments/environment.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "a", function() { return environment; });
// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `.angular-cli.json`.
// The file contents for the current environment will overwrite these during build.
var environment = {
    production: false
};
//# sourceMappingURL=environment.js.map

/***/ }),

/***/ "../../../../../src/main.ts":
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
Object.defineProperty(__webpack_exports__, "__esModule", { value: true });
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_0__angular_core__ = __webpack_require__("../../../core/@angular/core.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__ = __webpack_require__("../../../platform-browser-dynamic/@angular/platform-browser-dynamic.es5.js");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_2__app_app_module__ = __webpack_require__("../../../../../src/app/app.module.ts");
/* harmony import */ var __WEBPACK_IMPORTED_MODULE_3__environments_environment__ = __webpack_require__("../../../../../src/environments/environment.ts");




if (__WEBPACK_IMPORTED_MODULE_3__environments_environment__["a" /* environment */].production) {
    Object(__WEBPACK_IMPORTED_MODULE_0__angular_core__["_19" /* enableProdMode */])();
}
Object(__WEBPACK_IMPORTED_MODULE_1__angular_platform_browser_dynamic__["a" /* platformBrowserDynamic */])().bootstrapModule(__WEBPACK_IMPORTED_MODULE_2__app_app_module__["a" /* AppModule */])
    .catch(function (err) { return console.log(err); });
//# sourceMappingURL=main.js.map

/***/ }),

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__("../../../../../src/main.ts");


/***/ })

},[0]);
//# sourceMappingURL=main.bundle.js.map