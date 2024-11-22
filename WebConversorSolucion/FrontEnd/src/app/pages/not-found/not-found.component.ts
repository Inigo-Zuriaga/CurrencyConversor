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
  this.renderer.setStyle(this.doc.body,'background-image','url("../../../assets/CoinWeb.gif")');
    this.renderer.removeStyle(this.doc.body, 'animation'); // Cancela la animación
    this.renderer.removeStyle(this.doc.body, 'background-size');
  }


  ngOnDestroy() {
    this.renderer.setStyle(this.doc.body,'background','var(--background)');
    this.renderer.setStyle(this.doc.body,'background-size','var(--background-size)');
    this.renderer.setStyle(this.doc.body,'animation','var(--animation-gradient)');
  }
}





