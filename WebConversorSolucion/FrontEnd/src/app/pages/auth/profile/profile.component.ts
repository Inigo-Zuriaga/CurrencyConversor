import { Component } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css'
})
export class ProfileComponent {


  selectedPicture: string | null = null;

  selectProfilePicture(picture: string) {
    this.selectedPicture = picture;
    console.log(`Selected profile picture: ${picture}`);
  }

  confirmSelection() {
    if (this.selectedPicture) {
      console.log(`Confirmed profile picture: ${this.selectedPicture}`);
      // Add your logic to handle the confirmed profile picture
    } else {
      console.log('No profile picture selected');
    }
  }
}
