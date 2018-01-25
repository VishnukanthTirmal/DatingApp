import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { ActivatedRoute } from '@angular/router/src/router_state';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberDetialResolvers } from './_resolvers/member-detail.resolvers';
import { MemberListResolvers } from './_resolvers/member-list.resolvers';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolvers } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes';

export const appRoutes: Routes = [
{path: 'home', component: HomeComponent },
{
    path: '' ,
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
        {path: 'members', component: MemberListComponent, resolve: {users: MemberListResolvers } },
        {path: 'members/:id', component: MemberDetailComponent, resolve: {user: MemberDetialResolvers} },
        {path: 'member/edit', component: MemberEditComponent,
         resolve: {user: MemberEditResolvers}, canDeactivate: [PreventUnsavedChanges] },
        {path: 'messages', component: MessagesComponent },
        {path: 'lists', component: ListsComponent },
    ]
},
{path: '*', redirectTo: 'home', pathMatch: 'full'}
];
