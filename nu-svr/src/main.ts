require('source-map-support').install();
import * as express from "express";
import {App} from "./App"

console.log("STARTING...");

const app = new App();
app.run();

console.log("EXITING...");

