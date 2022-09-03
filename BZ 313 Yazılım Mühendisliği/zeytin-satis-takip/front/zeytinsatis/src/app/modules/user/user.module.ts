import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './pages/login/login.component';
// import { UserTemplateComponent } from './user-template/user-template.component';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule } from '@angular/forms';


import {
  AuthGuardService as AuthGuard
} from './../../core/guards/auth-guard.service';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { CreateSaleRecordComponent } from './pages/create-sale-record/create-sale-record.component';

const userRoutes: Routes = [
  // { path: 'register',  component: RegisterComponent },
  // { path: 'panel/login', component: LoginComponent },
  // // { path: 'user/:username',  component: ProfileComponent },
  // { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  // {
  //   path: 'register-new-user',
  //   component: RegisterUserComponent,
  //   canActivate: [RoleGuard],
  //   data: {
  //     expectedRole: 'admin'
  //   }
  // },
  // { path: '**', redirectTo: '' },
  { path: 'login', component: LoginComponent },
  {
    path: 'panel',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'panel',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'create-sale-record',
        component: CreateSaleRecordComponent,
        canActivate: [AuthGuard]
      },
      // {
      //   path: 'edit-monitors',
      //   component: EditMonitorsComponent,
      //   canActivate: [AuthGuard]
      // },
      // Admin Routes
      // {
      //   path: 'admin',
      //   children: [
      //     {
      //       path: 'users',
      //       component: RegisterUserComponent,
      //     },
      //     {
      //       path: 'monitors',
      //       component: MonitorsComponent,
      //     }
      //   ],
      //   data: {
      //     expectedRole: 'admin'
      //   },
      //   canActivate: [RoleGuard]
      // },

      // User routes

      { path: '**', redirectTo: '' }
    ]
  }
];

@NgModule({
  declarations: [
    LoginComponent,
    DashboardComponent,
    CreateSaleRecordComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forRoot(userRoutes
      // , { enableTracing: true }
    ),
  ],
  exports: [
    RouterModule
  ]
})
export class UserModule { }
