/* tslint:disable max-line-length */
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Observable, of } from 'rxjs';
import { HttpHeaders, HttpResponse } from '@angular/common/http';

import { McappWebTestModule } from '../../../test.module';
import { ThemaComponent } from 'app/entities/thema/thema.component';
import { ThemaService } from 'app/entities/thema/thema.service';
import { Thema } from 'app/shared/model/thema.model';

describe('Component Tests', () => {
    describe('Thema Management Component', () => {
        let comp: ThemaComponent;
        let fixture: ComponentFixture<ThemaComponent>;
        let service: ThemaService;

        beforeEach(() => {
            TestBed.configureTestingModule({
                imports: [McappWebTestModule],
                declarations: [ThemaComponent],
                providers: []
            })
                .overrideTemplate(ThemaComponent, '')
                .compileComponents();

            fixture = TestBed.createComponent(ThemaComponent);
            comp = fixture.componentInstance;
            service = fixture.debugElement.injector.get(ThemaService);
        });

        it('Should call load all on init', () => {
            // GIVEN
            const headers = new HttpHeaders().append('link', 'link;link');
            spyOn(service, 'query').and.returnValue(
                of(
                    new HttpResponse({
                        body: [new Thema(123)],
                        headers
                    })
                )
            );

            // WHEN
            comp.ngOnInit();

            // THEN
            expect(service.query).toHaveBeenCalled();
            expect(comp.themas[0]).toEqual(jasmine.objectContaining({ id: 123 }));
        });
    });
});
