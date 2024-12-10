export interface Midia {
  midiaId: number;
  urlMidia: string;
  tipo: string;
  ordem: number;
  anuncioAnimalId: number;
}

export interface Anuncio {
  anuncioId: number;
  titulo: string;
  descricao: string;
  nomeAnimal: string;
  tamanho: string | null;
  idade: number;
  especie: string | null;
  adotado: boolean;
  anuncianteId: string;
  midias: Midia[];
}
