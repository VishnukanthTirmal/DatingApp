import { NgxGalleryModule } from 'ngx-gallery';
import { MemberListResolvers } from './_resolvers/member-list.resolvers';
import { MemberDetialResolvers } from './_resolvers/member-detail.resolvers';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { UserService } from './_services/User.service';
import { AuthGuard } from './_guards/auth.guard';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './route';
import { MemberCardComponent } from './members/member-card/member-card.component';
import { AuthModule } from './auth/auth.module';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolvers } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes';




@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    RegisterComponent,
    MemberListComponent,
    ListsComponent,
    MessagesComponent,
    MemberCardComponent,
    MemberDetailComponent,
    MemberEditComponent
],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    AuthModule,
    TabsModule.forRoot(),
    NgxGalleryModule
  ],
  providers: [
    AuthService,
    AlertifyService,
    AuthGuard,
    UserService,
    MemberDetialResolvers,
    MemberListResolvers,
    MemberEditResolvers,
    PreventUnsavedChanges

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
