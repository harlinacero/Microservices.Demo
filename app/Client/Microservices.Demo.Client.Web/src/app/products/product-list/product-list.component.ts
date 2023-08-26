import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../services/data/products.service';
import { IProduct } from '../../models/iproduct';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss']
})
export class ProductListComponent implements OnInit {
  title!: string;
  products: IProduct[] = [];

  constructor(
    private productsService: ProductsService,
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.productsService.getProducts()
      .subscribe((response: IProduct[]) => {
        this.products = response;
      });
  }
}
