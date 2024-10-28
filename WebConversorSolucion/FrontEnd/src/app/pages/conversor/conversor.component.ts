import { Component ,OnInit} from '@angular/core';
import {Title} from '@angular/platform-browser';

@Component({
  selector: 'app-conversor',
  standalone: true,
  imports: [],
  templateUrl: './conversor.component.html',
  styleUrl: './conversor.component.css'
})
export class ConversorComponent implements OnInit{

  constructor(private title: Title) {
  }
  ngOnInit() {
    this.title.setTitle('Converor de Monedas');
  }
}
