import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PoliciesComponent } from './policies.component';
import { PolicyCreateComponent } from './policy-create/policy-create.component';
import { PolicyListComponent } from './policy-list/policy-list.component';



const routes: Routes = [
  {
    path: '', component: PoliciesComponent,
    children: [
      { path: 'list', component: PolicyListComponent},
      { path: 'fromOffer/:offerNumber', component: PolicyCreateComponent },
    ]
  }
];

@NgModule({
  declarations: [],
  exports: [RouterModule],
  imports: [
    RouterModule.forChild(routes),
    CommonModule
  ]
})
export class PoliciesRoutingModule {
  static components = [
    PolicyCreateComponent,
    PolicyListComponent
  ];
}
