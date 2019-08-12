import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { CartPopupComponent } from '../cart-popup/cart-popup';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})

export class FetchDataComponent {
  public products: ProductItem[];
  public cartItems: CartItem[] = [];
  public cartItemCount: number = 0;
  public addClass: any;
  public addStyle: any;
  public salesTax: any;
  public total: any;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ProductItem[]>(baseUrl + 'api/SampleData/GetProducts').subscribe(result => {
      this.products = result;
      this.products.forEach(i => {
        i.currentCount = 0;
      });
    }, error => console.error(error));
  }

  public incrementCounter(product: any) {
    product.currentCount++;
  }

  public decrementCounter(product: any) {
    product.currentCount = product.currentCount > 0 ? product.currentCount - 1 : 0;
  }

  public addToCart(product: any) {
    if (product.currentCount === 0)
      return;

    
    var item = this.cartItems.filter(i => i.product.id === product.id);
    if (item.length > 0) {
      item[0].qty += product.currentCount;
    }
    else {
      this.cartItems.push(new CartItem(product, product.currentCount));
    }
    
    this.cartItemCount = 0;
    this.cartItems.forEach(i => {
      this.cartItemCount += i.qty;
    });
    product.currentCount = 0;//After adding change count back to 0
  }

  public showCart() {
    this.addClass = "show block";
    this.addStyle = "display:block;";
    this.salesTax = 0;
    this.total = 0;
    this.cartItems.forEach(i => {
      this.salesTax += i.product.salesTax * i.qty;
      this.total += i.product.grossPrice * i.qty;      
    });
  }

  public closeCart() {
    this.addClass = "";
    this.addStyle = ""
  }
}

class ProductItem {
  id: number;
  importDuty: number;
  unitPrice: number;
  basicSalesTax: number;
  itemName: string;
  currentCount: number;
  grossPrice: number;
  salesTax: number;
}

class CartItem {
  product: ProductItem;
  qty: number;

  constructor(product: ProductItem, pQty: number) {
    this.product = product;
    this.qty = pQty;
  }
}


