import { Component,Input  } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-historytable',
  templateUrl: './historytable.component.html',
  styleUrl: './historytable.component.css'
})
export class HistorytableComponent {
@Input() historyData: any[] = [];
@Input() limit: number | null = null;

constructor(private authService: AuthService) {
}
  get displayedData() {
    // Retorna todos los datos o un subconjunto limitado
    return this.limit ? this.historyData.slice(0, this.limit) : this.historyData;
  }

}
