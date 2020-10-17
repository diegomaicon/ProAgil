import { RedeSocial } from './RedeSocial';
import { Evento } from './evento';

export interface Palestrante {
    id: number;
    nome: string;
    miniCurriculo: string;
    imagemURL: string;
    telefone: string;
    email: string;
    redeSocial: RedeSocial[];
    palestranteEvento: Evento[];
}

