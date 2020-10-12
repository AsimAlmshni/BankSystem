import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomersListComponent } from './customers-list/customers-list.component'  
import { CreateEditCustomerComponent } from './create-edit-customer/create-edit-customer.component';
import { MaterialModule } from './material-module/material.module';


@NgModule({
  declarations: [
    AppComponent,
    CustomersListComponent,
    CreateEditCustomerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule.forRoot([
      { 
        path: '',
        component: CustomersListComponent,
        pathMatch: 'full'
      }, {
        path: 'customers',
        component: CustomersListComponent,
      }, {
        path: 'customers/:id',
        component: CreateEditCustomerComponent
      }, {
        path: 'customers/create',
        component: CreateEditCustomerComponent
      }
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
