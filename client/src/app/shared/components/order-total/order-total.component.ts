import { Component, OnInit, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasketTotals } from '../../models/basket';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-order-total',
  templateUrl: './order-total.component.html',
  styleUrls: ['./order-total.component.scss']
})
export class OrderTotalComponent implements OnInit {
  basketTotal$: Observable<IBasketTotals>;
  constructor(private basketService: BasketService) { }
  @Input() shippingPrice: number;
  @Input() subtotal: number;
  @Input() total: number;

  ngOnInit(): void {
    this.basketTotal$ = this.basketService.basketTotal$;
  }

}
