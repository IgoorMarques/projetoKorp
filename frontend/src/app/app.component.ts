import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {MatToolbarModule} from '@angular/material/toolbar';
import { HeaderComponent } from "./shared/header/header.component";
import { BannerComponent } from './shared/banner/banner.component';
import { FooterComponent } from './shared/footer/footer.component';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [MatToolbarModule, HeaderComponent, RouterOutlet, FooterComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend_korp';
}
