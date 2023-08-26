import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PoliciesRoutingModule } from './policies-routing.module';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [PoliciesRoutingModule.components],
  imports: [
    CommonModule,
    SharedModule,
    PoliciesRoutingModule
  ]
})
export class PoliciesModule { }
