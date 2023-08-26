import { Component, OnInit, Input, ViewChild, OnChanges, SimpleChanges } from '@angular/core';
import { IProduct } from '../../models/iproduct';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ICover } from '../../models/icover';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit, OnChanges {
  @Input() product!: IProduct;
  displayedColumns: string[] = ['Position', 'Code', 'Name', 'Optional','SumInsured'];
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  dataSource!: MatTableDataSource<ICover>;

  constructor() {
    
  }

  ngOnInit(): void {
    
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.loadData();
  }

  loadData() {
    this.dataSource = new MatTableDataSource(this.product.Covers);
    this.dataSource.paginator = this.paginator;
  }
}
