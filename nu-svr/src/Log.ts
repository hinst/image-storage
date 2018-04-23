import { Z_UNKNOWN } from "zlib";

const dateFormat = require('dateformat');
export enum LogLevel {
    info = "info"
}
export class Log {
    filePath: string;
    constructor(filePath: string) {
        this.filePath = filePath;
    }
    info(text: any) {
        this.write(LogLevel.info, text);
    }
    write(level: LogLevel, text: any) {
        console.log(dateFormat(new Date(), "yyyy.mm.dd-HH:MM:ss.l")  + " " + level.toString().toUpperCase() + ": " + text);
    }
}