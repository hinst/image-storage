namespace React {
    var UID: number = 0;
    const idPrefix = "react_";
    function addContents(element: HTMLElement, contents: any[]) {
        for (var content of contents) {
            if (typeof content == "string")
                element.innerHTML += content;
            if (typeof content == "object")
                $(element).append(content);
        }
    }

    function setAttributes(element: HTMLElement, attributes) {
        for (var attr in attributes) {
            var value = attributes[attr];
            if (typeof value == "string") {
                element.setAttribute(attr, value);
            }
        }
    }

    export function createElement(tag, attributes, ...contents) {
        const element: HTMLElement = document.createElement(tag);
        ++UID;
        element.id = idPrefix + UID;
        setAttributes(element, attributes);
        addContents(element, contents);
        return element;
    }
}