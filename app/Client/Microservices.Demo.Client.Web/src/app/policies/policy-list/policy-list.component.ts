import { Component, OnInit } from '@angular/core';
import { PoliciesService } from 'src/app/services/data/policies/policies.service';
import { IPolicy } from '../../models/iproduct';

@Component({
    selector: 'app-policy-list',
    templateUrl: './policy-list.component.html',
    styleUrls: [ './policy-list.component.scss' ]
})
export class PolicyListComponent implements OnInit {
    title!: string;
    policies: IPolicy[] = [];

    constructor(
        private policiesService: PoliciesService,
    ) { }

    ngOnInit(): void {
        this.getProducts();
    }

    getProducts() {
        this.policiesService.getPolicies()
            .subscribe((response: IPolicy[]) => {
                this.policies = response;
            });
    }
}
