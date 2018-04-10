/// <reference path="Widget"/>

namespace hImageStorage {
    export class Gallery extends Widget {
        imageUI: HTMLElement;
        list: string[];
        init() {
            this.element = $(
                <div>
                    <button class="w3-btn w3-black">&lt;</button>
                    <button class="w3-btn w3-black">&gt;</button>
                    <hr style="margin-top: 0px; margin-bottom: 0px;"/>
                    {this.imageUI = <img alt="gallery image" class="h-image-storage-gallery-image"/>}
                </div>
            );
            this.loadList();
        }

        loadList() {
            $.ajax({
                url: this.appPath + "/GetImages",
                success: $.proxy(this.receiveList, this)
            });
        }

        receiveList(data: string[]) {
            this.list = data;
            if (!this.imageUI.getAttribute("src") && this.list.length > 0) {
                this.loadImage(this.list[0]);
            }
        }

        loadImage(id: string) {
            $.ajax({
                url: this.appPath + "/GetImage?id=" + encodeURIComponent(id),
                success: $.proxy(this.receiveImage, this),
            });
            this.imageUI.setAttribute("src", this.appPath + "/GetImageData?id=" + encodeURIComponent(id));
        }

        receiveImage(imageObject: WebImage) {
            console.log(imageObject.originalFileName);
        }
    }
}