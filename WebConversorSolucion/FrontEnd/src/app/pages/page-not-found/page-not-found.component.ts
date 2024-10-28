import { Component ,OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {Title} from '@angular/platform-browser';
@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [
    RouterLink
  ],
  templateUrl: './page-not-found.component.html',
  styleUrl: './page-not-found.component.css'
})
export class PageNotFoundComponent implements OnInit{

  constructor(private router: Router,
              private title: Title) {
  }
  ngOnInit() {
    this.title.setTitle('404 - Página no encontrada');
  }
}
