import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {


  selectedPicture: string = '';

  constructor(private authService: AuthService) {
  }
  selectProfilePicture(picture: string) {
    this.selectedPicture = picture;
    console.log(`Selected profile picture: ${picture}`);
  }
  confirmSelection() {
    if (this.selectedPicture) {
      console.log(`Confirmed profile picture: ${this.selectedPicture}`);

      this.authService.changePicture(this.authService.getUserEmail(), this.selectedPicture).subscribe(
        (data) => {
          console.log("Foto actualizada correctamente en el backend:", data);

          // Actualizamos el BehaviorSubject con la nueva foto de perfil
          this.authService.photoSubject.next(this.selectedPicture);
        },
        (error) => {
          console.error("Error al cambiar la foto de perfil:", error);
        }
      );
    } else {
      console.log('No profile picture selected');
    }
  }
  // confirmSelection() {
  //   if (this.selectedPicture) {
  //     console.log(`Confirmed profile picture: ${this.selectedPicture}`);
  //
  //     this.authService.getUserData().subscribe(
  //       (data) => {
  //         console.log("Datos recibidos:", data);
  //
  //         this.authService.photoSubject.next(data);
  //       },
  //       (error) => {
  //         console.error("Error al obtener el historial:", error);
  //     }
  //     );
  //
  //     this.authService.changePicture(this.authService.getUserEmail(), this.selectedPicture).subscribe(
  //       (data) => {
  //         console.log("Datos recibidos:", data);
  //       },
  //       (error) => {
  //         console.error("Error al obtener el historial:", error);
  //     }
  //     );
  //
  //     // Add your logic to handle the confirmed profile picture
  //   } else {
  //     console.log('No profile picture selected');
  //   }
  // }
}
