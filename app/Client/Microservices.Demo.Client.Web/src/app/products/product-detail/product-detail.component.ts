import { Component, OnInit, ViewChild, SimpleChanges } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ProductsService } from '../../services/data/products.service';
import { IProduct } from '../../models/iproduct';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ICover } from '../../models/icover';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { MAT_DATE_FORMATS, DateAdapter, MAT_DATE_LOCALE } from '@angular/material/core';
import { MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { IProductDetail } from '../../models/iproduct-detail';
import { IQuestionAnswer } from '../../models/iquestion-answer';
import { IQuestion } from '../../models/iquestion';
import { ICreateOfferCommand } from '../../services/data/offers/icreate-offer-command';
import { strict } from 'assert';
import { OffersService } from '../../services/data/offers/offers.service';
import { ICreateOfferResult } from '../../services/data/offers/icreate-offer-result';

export const DD_MM_YYYY_Format = {
  parse: {
    dateInput: 'LL',
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};


@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS]
    },
    { provide: MAT_DATE_FORMATS, useValue: DD_MM_YYYY_Format },
  ]
})
export class ProductDetailComponent implements OnInit {
  product!: IProduct;
  displayedColumns: string[] = ['Position', 'Code', 'Name', 'Optional', 'SumInsured'];
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource!: MatTableDataSource<ICover>;
  policyForm!: FormGroup;
  mode: string = 'EDIT';
  offer: ICreateOfferResult = { TotalPrice: 0, CoversPrices: null, OfferNumber: '' };

  constructor(
    private route: ActivatedRoute,
    private productsService: ProductsService,
    private offersService: OffersService,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    this.buildForm();

    this.route.params.subscribe((params: Params) => {
      let code = params['code'];
      this.getProductByCode(code);
    });
  }

  IsReadOnly():boolean {
    return (this.mode == 'VIEW');
  }

  buildForm() {
    this.policyForm = this.formBuilder.group({
      PolicyFrom: ['', []],
      PolicyTo: ['', []],
      QuestionsAnswersControls: new FormArray([])
    });
  }

  getProductByCode(productCode:string) {
    this.productsService.getProductByCode(productCode)
      .subscribe((response: IProduct) => {
        this.product = response;
        this.setQuestionsAnswersControls(this.product.Questions);
        this.loadData();
      });
  }

  setQuestionsAnswersControls(questions: IQuestion[]) {    
    questions.forEach(question => {
      this.QuestionsAnswersControls.push(new FormControl('',[]));
    });
  }

  getQuestionsAnswers(): IQuestionAnswer[]{
    let questionsAnswers= new Array<IQuestionAnswer>();

    for (var i = 0; i < this.QuestionsAnswersControls.length; i++) {
      questionsAnswers.push({
        QuestionCode: this.product.Questions[i].QuestionCode,
        QuestionType: this.product.Questions[i].QuestionType,
        Answer:this.QuestionsAnswersControls.controls[i].value
      });
    }

    return questionsAnswers;
  }

  getCovers(): string[] {
    let covers = new Array<string>();

    this.product.Covers.forEach(cover => {
      covers.push(cover.Code);
    });

    return covers;
  }

  get QuestionsAnswersControls(): FormArray {
    return this.policyForm.get('QuestionsAnswersControls') as FormArray;
  }

  loadData() {
    this.dataSource = new MatTableDataSource(this.product.Covers);
    this.dataSource.paginator = this.paginator;
  }

  changeParameters() {
    this.policyForm.enable();
    this.mode = 'EDIT';
  }

  submit({ value, valid }: { value: IProductDetail, valid: boolean }) {
    if (!this.IsReadOnly()) {
      this.policyForm.disable();

      let createOfferCommand: ICreateOfferCommand = {
        PolicyFrom: this.policyForm.get("PolicyFrom")!.value.toDate(),
        PolicyTo: this.policyForm.get("PolicyTo")!.value.toDate(),
        ProductCode: this.product.Code,
        SelectedCovers: this.getCovers(),
        Answers: this.getQuestionsAnswers()
      };

      this.offersService.calculatePrice(createOfferCommand)
        .subscribe((response: ICreateOfferResult) => {
          this.offer = response;
        });

      this.mode = 'VIEW'
    } else {
      this.mode = 'EDIT'
    }
    
    
  }

}
