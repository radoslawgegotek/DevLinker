import { NgModule } from '@angular/core';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { SidemenuComponent } from './sidemenu/sidemenu.component';
import { FooterComponent } from './footer/footer.component';
import { NotFoundComponent } from './not-found/not-found.component';

const components = [
  NavbarComponent,
  HomeComponent,
  SidemenuComponent,
  FooterComponent,
  NotFoundComponent
]

@NgModule({
  declarations: [],
  imports: components,
  exports: components
})
export class MainModule { }
