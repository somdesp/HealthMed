import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, CanActivateChild, RouterStateSnapshot } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable()
export class RoleGuardService implements CanActivate, CanActivateChild {

    constructor(public auth: AuthService, public router: Router) { }

    canActivate(route: ActivatedRouteSnapshot): boolean {

        const expectedRoleValue = route.data['expectedRoleValue'];
        let isValid: boolean = false;
        const RolesUser = this.auth.GetJwtAcess();
        //Valida os acessos do Usuario as rotas
        if (RolesUser.length == 0 || RolesUser != expectedRoleValue) {
            return false;
        }

        return true;
    }

    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

        const expectedRoleValue = childRoute.data['expectedRoleValue'];
        let isValid: boolean = false;
        const RolesUser = this.auth.GetJwtAcess();
        //Valida os acessos do Usuario as rotas
        if (RolesUser.length == 0 || RolesUser != expectedRoleValue) {
            return false;
        }

        if (!this.auth.isAuthenticated() || !isValid) {
            this.router.navigate(['']);
            return false;
        }

        return true;
    }

}