/* tslint:disable max-line-length */
import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

import { McappWebTestModule } from '../../../test.module';
import { QuizFrageUpdateComponent } from 'app/entities/quiz-frage/quiz-frage-update.component';
import { QuizFrageService } from 'app/entities/quiz-frage/quiz-frage.service';
import { QuizFrage } from 'app/shared/model/quiz-frage.model';

describe('Component Tests', () => {
    describe('QuizFrage Management Update Component', () => {
        let comp: QuizFrageUpdateComponent;
        let fixture: ComponentFixture<QuizFrageUpdateComponent>;
        let service: QuizFrageService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [QuizFrageUpdateComponent]
            })
                .overrideTemplate(QuizFrageUpdateComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(QuizFrageUpdateComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(QuizFrageService);
        });

        describe('save', () => {
            it(
                'Should call update service on save for existing entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new QuizFrage(123);
                    spyOn(service, 'update').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.quizFrage = entity;
                    // WHEN
                    comp.save();
                    tick(); // simulate async

                    // THEN
                    expect(service.update).toHaveBeenCalledWith(entity);
                    expect(comp.isSaving).toEqual(false);
                })
            );

            it(
                'Should call create service on save for new entity',
                fakeAsync(() => {
                    // GIVEN
                    const entity = new QuizFrage();
                    spyOn(service, 'create').and.returnValue(of(new HttpResponse({ body: entity })));
                    comp.quizFrage = entity;
                    // WHEN
                    comp.save();
                    tick(); // simulate async

                    // THEN
                    expect(service.create).toHaveBeenCalledWith(entity);
                    expect(comp.isSaving).toEqual(false);
                })
            );
        });
    });
});
