import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {


  selectedPicture: string | null = null;

  constructor(private authService: AuthService) {
  }
  selectProfilePicture(picture: string) {
    this.selectedPicture = picture;
    console.log(`Selected profile picture: ${picture}`);
  }

  confirmSelection() {
    if (this.selectedPicture) {
      console.log(`Confirmed profile picture: ${this.selectedPicture}`);

      this.authService.getUserData().subscribe(
        (data) => {
          console.log("Datos recibidos:", data);
        },
        (error) => {
          console.error("Error al obtener el historial:", error);
      }
      );

      this.authService.changePicture(this.authService.getUserEmail(), this.selectedPicture).subscribe(
        (data) => {
          console.log("Datos recibidos:", data);
        },
        (error) => {
          console.error("Error al obtener el historial:", error);
      }
      );

      // Add your logic to handle the confirmed profile picture
    } else {
      console.log('No profile picture selected');
    }
  }
}
