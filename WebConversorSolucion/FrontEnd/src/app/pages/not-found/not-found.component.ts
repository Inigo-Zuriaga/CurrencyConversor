import {Component, Inject, Renderer2,OnInit,OnDestroy} from '@angular/core';
import {DOCUMENT}  from '@angular/common';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.css'
})
export class NotFoundComponent implements OnInit, OnDestroy {
constructor(private renderer:Renderer2,@Inject(DOCUMENT) private doc: Document) { }



  ngOnInit() {
  // this.renderer.setStyle(this.doc.body,'background','#fff');
  //   background-image: url("../../../assets/moneda.png");
    this.renderer.removeStyle(this.doc.body, 'position');
    this.renderer.removeStyle(this.doc.body, 'z-index');
    this.renderer.removeStyle(this.doc.body, 'overflow');
    this.renderer.removeStyle(this.doc.body, 'width');
    this.renderer.removeStyle(this.doc.body, 'height');
    this.renderer.removeStyle(this.doc.body, 'background-color');
    this.renderer.setStyle(this.doc.body, 'background-color', '#fef3c7');
    this.renderer.setStyle(this.doc.body,'background-image','url("../../../assets/CoinWeb.gif")');
    this.renderer.removeStyle(this.doc.body, 'animation'); // Cancela la animación
    // this.renderer.removeStyle(this.doc.body, 'canvas');


    // this.renderer.setStyle(this.doc.body, 'no-background');
    // this.renderer.removeClass(this.doc.body, 'unique-page-id');
  }


  ngOnDestroy() {
    // this.renderer.setStyle(this.doc.body,'background','var(--background)');
    this.renderer.setStyle(this.doc.body,'background-size','var(--background-size)');
    // this.renderer.setStyle(this.doc.body,'animation','var(--animation-gradient)');
    this.renderer.setStyle(this.doc.body, 'position', 'absolute');
    this.renderer.setStyle(this.doc.body, 'z-index', '-1');
    this.renderer.setStyle(this.doc.body, 'overflow', 'hidden');
    this.renderer.setStyle(this.doc.body, 'width', '100vw');
    this.renderer.setStyle(this.doc.body, 'height', '100vh');


    // this.renderer.setStyle(this.doc.body, 'background-color', '#fef3c7');
    // this.renderer.setStyle(this.doc.body, 'no-background', 'none');
    // this.renderer.setStyle(this.doc.body, 'unique-page-id', 'none');
  }
}





