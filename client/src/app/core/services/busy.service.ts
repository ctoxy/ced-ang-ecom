import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.busyRequestCount++;
    // https://napster2210.github.io/ngx-spinner/ différent sinner existant
    this.spinnerService.show(undefined, {
      type: 'square-jelly-box',
      bdColor: 'rgba(255,255,255, 0.7)',
      color: '#333333'
    })
  }

  idle(){
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0 ) {
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
