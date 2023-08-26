import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, NgForm } from '@angular/forms';
import { ActivatedRoute, Params } from '@angular/router';
import { ICreatePolicyCommand } from '../../services/data/policies/icreate-policy-command';
import { PoliciesService } from '../../services/data/policies/policies.service';
import { ICreatePolicyResult } from '../../services/data/policies/icreate-policy-result';

@Component({
  selector: 'app-policy-create',
  templateUrl: './policy-create.component.html',
  styleUrls: ['./policy-create.component.scss']
})
export class PolicyCreateComponent implements OnInit {
  policyForm!: FormGroup;
  Countries: string[] = ['Poland', 'France', 'Germany'];
  offerNumber: string = '';
  policyNumber: string = '';

  constructor(
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private policiesService: PoliciesService,
  ) { }

  ngOnInit(): void {
    this.buildForm();

    this.route.params.subscribe((params: Params) => {
      this.offerNumber = params['offerNumber'];
    });
  }

  buildForm() {
    this.policyForm = this.formBuilder.group({
      FirstName: ['', []],
      LastName: ['', []],
      TaxId: ['', []],
      Country: ['', []],
      ZipCode: ['', []],
      City: ['', []],
      Street: ['', []]
    });

    this.policyForm.get('Country')!.setValue('Poland');
  }

  submit({ value, valid }: { value: any, valid: boolean }) {
    let createPolicyCommand: ICreatePolicyCommand = {
      OfferNumber: this.offerNumber,
      PolicyHolder: {
        FirstName: this.policyForm.get("FirstName")!.value,
        LastName: this.policyForm.get("LastName")!.value,
        TaxId: this.policyForm.get("TaxId")!.value
      },
      PolicyHolderAddress: {
        City: this.policyForm.get("City")!.value,
        Country: this.policyForm.get("Country")!.value,
        ZipCode: this.policyForm.get("ZipCode")!.value,
        Street: this.policyForm.get("Street")!.value
      }
    };

    this.policiesService.CreatePolicy(createPolicyCommand)
      .subscribe((response: ICreatePolicyResult) => {
        this.policyNumber = response.PolicyNumber;
      });
  }

}
