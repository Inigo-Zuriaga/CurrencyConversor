import { Component,Input } from '@angular/core';
import { Router } from '@angular/router';
import {Idropdownoption} from '../../../Interfaces/idropdownoption';
@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrl: './dropdown.component.css'
})


export class DropdownComponent {
@Input() buttonClass:string='';
@Input() dropdownClass:string='';
@Input() buttonImageUrl: string | null = null;
@Input() options: any[] = [];
@Input() useRouter: boolean = false;


  isOpen = false;

  constructor(private router: Router) {}
  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

  onSelectOption(option: Idropdownoption) {
    if (option.route && this.useRouter) {
      this.router.navigate([option.route]);  // Navega si hay una ruta
    } else if (option.callback && typeof option.callback === 'function') {
      option.callback();  // Llama al callback si es una función
    }
    this.isOpen = false;  // Cierra el dropdown
  }
}
