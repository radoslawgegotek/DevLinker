import { Routes } from '@angular/router';
import { HomeComponent } from './main/home/home.component';
import { NotFoundComponent } from './main/not-found/not-found.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'account',
    loadChildren: () => import('./feature/account/account.routes').then(x => x.account_routes)
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
  {
    path: '**',
    component: NotFoundComponent
  }
];
