/// <reference path="Widget"/>

namespace hImageStorage {
    export class Gallery extends Widget {
        img: JQuery;
        init() {
            this.element = $(
                <div>
                    <button class="w3-btn w3-black">&lt;</button>
                    <button class="w3-btn w3-black">&gt;</button>
                    <hr/>
                    {this.img = <img alt="gallery image" class="h-image-storage-gallery-image"/>}
                </div>
            );
            
        }


    }
}