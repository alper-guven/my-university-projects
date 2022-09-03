
export namespace Product {

    export interface CreateProduct{

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

    export interface ZeytinBilgisi{
        tur: string,
        yagMiktari: string,
        orijin: string,
        etCekirdekOrani: string
    }
    
} 