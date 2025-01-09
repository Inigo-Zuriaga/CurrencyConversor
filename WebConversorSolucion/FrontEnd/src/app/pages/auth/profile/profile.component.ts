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

  async confirmSelection() {
    if (this.selectedPicture) {
      try {
        // Llama a la API para cambiar la imagen en el backend
        const email = this.authService.getUserEmail();
        const response = await this.authService.changePicture(email, this.selectedPicture).toPromise();

        console.log("Foto actualizada correctamente en el backend:", response);

        // Llama al backend para obtener la imagen actualizada desde la base de datos
        const updatedUserData = await this.authService.getUserData().toPromise();

        // Actualiza el BehaviorSubject con la nueva URL de la foto
        this.authService.photoSubject.next(updatedUserData.img);
      } catch (error) {
        console.error("Error al actualizar la foto de perfil:", error);
      }
    } else {
      console.log('No profile picture selected');
    }
  }
}
