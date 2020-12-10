import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Contants } from '../util/Contants';

@Pipe({
  name: 'DataTimeFormatPipe'
})
export class DataTimeFormatPipePipe extends DatePipe implements PipeTransform {

  transform(value: any, args?: any): any {
    return super.transform(value, Contants.DATA_TIME_FMT);
  }

}
