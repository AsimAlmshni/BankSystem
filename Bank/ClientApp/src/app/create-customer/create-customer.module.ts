import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CreateCustomerComponent } from './create-customer.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    CreateCustomerComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class CreateCustomerModule { }
