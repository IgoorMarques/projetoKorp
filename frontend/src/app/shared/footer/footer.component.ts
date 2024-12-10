import { Component } from '@angular/core';
import { ContainerComponent } from '../container/container.component';
import { MatButtonModule } from '@angular/material/button';
import { LogoComponent } from "../header/logo/logo.component";

@Component({
  selector: 'app-footer',
  imports: [ContainerComponent, MatButtonModule, LogoComponent],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss'
})
export class FooterComponent {

}
