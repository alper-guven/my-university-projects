
import mongoose, { Schema, Document } from 'mongoose';

const uuidv4 = require('uuid/v4');

export interface ISale extends Document{

    uuid: string,
    
    hammaddeBilgisi: {
        zeytin: {
            tur: string,
            yagMiktari: string,
            orijin: string,
            etCekirdekOrani: string
        },
        fabrikaGirisTarihi: string,
        uretici: string
    },

    fabrikaIslemleri: {

        ezmeIslemiSuresi: number,
        malakatorSicakligi: number,
        malakasyonSuresi: number,
        kiloBasinaSuMiktari: number,
        perkolasyon: string,
        primaNemMiktari: number,
        partiBilgisi: string,
        partideIslenenUrunSayisi: number

    },

    distributorIslemleri: {

        distrubutorAdi: string,
        alanKisi: string,
        gondermeTuru: string,
        urunMiktari: number,
        sevkiyatTarihi: string,
        tahminiSevkiyatSuresi?: string,
        sevkiyatSorunlari?: string

    },

    satisIslemleri: {

        teslimAlinanTarih: string,
        urunSayisi: number

    }

}

const saleSchema = new Schema({

        uuid: String,

        hammaddeBilgisi: {
            zeytin: {
                tur: String,
                yagMiktari: String,
                orijin: String,
                etCekirdekOrani: String
            },
            fabrikaGirisTarihi: String,
            uretici: String
        },

        fabrikaIslemleri: {

            ezmeIslemiSuresi: Number,
            malakatorSicakligi: Number,
            malakasyonSuresi: Number,
            kiloBasinaSuMiktari: Number,
            perkolasyon: String,
            primaNemMiktari: Number,
            partiBilgisi: String,
            partideIslenenUrunSayisi: Number

        },

        distributorIslemleri: {

            distrubutorAdi: String,
            alanKisi: String,
            gondermeTuru: String,
            urunMiktari: Number,
            sevkiyatTarihi: String,
            tahminiSevkiyatSuresi: String,
            sevkiyatSorunlari: String

        },

        satisIslemleri: {

            teslimAlinanTarih: String,
            urunSayisi: Number

        }

}, { timestamps: true });


/**
 * UUID middleware.
 */

saleSchema.pre<ISale>('save', function save(next) {

    let sale = this;

    sale.uuid = uuidv4();

    return next();

});

const User = mongoose.models.Sale || mongoose.model<ISale>('Sale', saleSchema);

export default User;