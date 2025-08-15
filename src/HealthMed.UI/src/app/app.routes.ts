import { Routes } from '@angular/router';
import { PacienteComponent } from './pages/paciente/agendamentos-paciente/paciente.component';
import { authGuard } from './core/guards/auth.guard';
import { MedicoLoginComponent } from './login/medico-login/medico-login.component';
import { PacienteLoginComponent } from './login/paciente-login/paciente-login.component';
import { MedicoComponent } from './pages/medico/agendamentos-medico/medico.component';
import { RoleGuardService } from './core/guards/role-guard.service';
import { LoginComponent } from './login/login/login.component';

export const appRoutes: Routes = [

    {
        path: '',
        component: LoginComponent,
        title: 'Login',
    },
    {
        path: 'login/medico',
        component: MedicoLoginComponent,
        title: 'Login',
    },
    {
        path: 'login/paciente',
        component: PacienteLoginComponent,
        title: 'Login',
    },
    {
        path: 'paciente',
        component: PacienteComponent,
        canActivate: [RoleGuardService],
        data: {
            expectedRoleValue: "Paciente"
        },
        title: 'Dashboard',
    },
    {
        path: 'medico',
        component: MedicoComponent,
        canActivate: [RoleGuardService],
        data: {
            expectedRoleValue: "Medico"
        },
        title: 'Dashboard',
    },
    {
        path: '**',
        redirectTo: '',
        pathMatch: 'full'
    },
];
