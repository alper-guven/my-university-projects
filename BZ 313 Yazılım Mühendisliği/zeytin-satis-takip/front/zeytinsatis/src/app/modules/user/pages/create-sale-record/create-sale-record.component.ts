import { Component, OnInit } from '@angular/core';

import { Product } from "../../product.model";
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';


const zeytinTurBilgileri: Array<Product.ZeytinBilgisi> = [
  {
    tur: 'Ayvalık',
    yagMiktari: '24.7',
    orijin: 'Ayvalık',
    etCekirdekOrani: 'orta'
  },
  {
    tur: 'Gemlik',
    yagMiktari: '29.0',
    orijin: 'Bursa-Gemlik',
    etCekirdekOrani: 'yüksek'
  },
  {
    tur: 'Domat',
    yagMiktari: '20.6',
    orijin: 'Manisa-Akhisar',
    etCekirdekOrani: 'yüksek'
  },
  {
    tur: 'Memecik',
    yagMiktari: '28.6',
    orijin: 'Muğla',
    etCekirdekOrani: 'yüksek'
  },
  {
    tur: 'Ödemiş',
    yagMiktari: '22.8',
    orijin: 'İzmir-Ödemiş',
    etCekirdekOrani: 'yüksek'
  },
  {
    tur: 'Sarı Ulak',
    yagMiktari: '18.8',
    orijin: 'Mersin-Tarsus',
    etCekirdekOrani: 'orta'
  },
  {
    tur: 'Arbequina',
    yagMiktari: '15.3',
    orijin: 'İspanya-Katalonya',
    etCekirdekOrani: 'düşük'
  },
  {
    tur: 'Girit',
    yagMiktari: '24.9',
    orijin: 'Ege-Sahil',
    etCekirdekOrani: 'orta'
  }

]

const sectionNames : Array<string> = [
  'hammaddeBilgisi',
  'fabrikaIslemleri',
  'distributorIslemleri',
  'satisIslemleri'
]

@Component({
  selector: 'app-create-sale-record',
  templateUrl: './create-sale-record.component.html',
  styleUrls: ['./create-sale-record.component.scss']
})
export class CreateSaleRecordComponent implements OnInit {

  publishErrorMessage: string = '';
  publishSuccessMessage: string = '';

  zeytinTurBilgileri = zeytinTurBilgileri;

  selectedSectionName = 'hammaddeBilgisi';

  sectionIndex = 0;

  newProduct: Product.CreateProduct = {

    hammaddeBilgisi: {
      zeytin: {
        tur: 'Ayvalık',
        yagMiktari: '',
        orijin: '',
        etCekirdekOrani: ''
      },
      fabrikaGirisTarihi: '',
      uretici: ''
    },

    fabrikaIslemleri: {

      ezmeIslemiSuresi: null,
      malakatorSicakligi: null,
      malakasyonSuresi: null,
      kiloBasinaSuMiktari: null,
      perkolasyon: '',
      primaNemMiktari: null,
      partiBilgisi: '',
      partideIslenenUrunSayisi: null

    },

    distributorIslemleri: {

      distrubutorAdi: '',
      alanKisi: '',
      gondermeTuru: 'kargo',
      urunMiktari: null,
      sevkiyatTarihi: '',
      tahminiSevkiyatSuresi: '',
      sevkiyatSorunlari: ''

    },

    satisIslemleri: {

      teslimAlinanTarih: '',
      urunSayisi: null

    }

  };

  constructor(
    private http: HttpClient,
  ) { }

  ngOnInit() {
  }

  onZeytinTuruChange(tur: string) {

    console.log(this.newProduct.hammaddeBilgisi.zeytin.tur)

    this.newProduct.hammaddeBilgisi.zeytin = zeytinTurBilgileri.filter(obj => {
      return obj.tur === tur
    })[0];

    console.log(zeytinTurBilgileri.filter(obj => {
      return obj.tur === tur
    }))

  }


  onClickNextSection(){

    this.sectionIndex++;

    this.selectedSectionName = sectionNames[this.sectionIndex];

  }

  onClickPrevSection(){
    
    this.sectionIndex--;

    this.selectedSectionName = sectionNames[this.sectionIndex];

  }

  onClickSubmit(){

    console.log(this.newProduct);
    
    this.postSaleRecord().subscribe(  (res) =>{

      if (res['success'] == true) {
        
        this.publishSuccessMessage = 'Satış takibi başarıyla oluşturuldu';

        setTimeout(() => {
          
          this.publishSuccessMessage = '';

        }, 8000);

      }else{

        this.publishErrorMessage = 'Satış takibi oluşturulamadı. Bir hata oluştu.';

        setTimeout(() => {
          
          this.publishErrorMessage = '';

        }, 10000);

      }

    })

  }

  postSaleRecord() {

    return this.http.post(environment.API.endpointURL + 'sales/', this.newProduct);

  }

}
